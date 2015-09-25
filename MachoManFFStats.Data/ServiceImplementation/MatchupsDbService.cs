using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MachoManFFStats.BLL;
using MachoManFFStats.BLL.Models;
using MachoManFFStats.Data.Entity;

namespace MachoManFFStats.Data.ServiceImplementation
{
    public class MatchupsDbService : IMatchupsService
    {
        private readonly MachoManFFEntities _context;

        private Func<vHistoricalMatchup, Matchup> _mapper = matchup => new Matchup()
        {
            MatchupId = matchup.MatchupID,
            Week = matchup.Week,
            Year = matchup.Year,
            IsTie = matchup.HomeScore == matchup.AwayScore,
            Teams = new List<MatchupTeam>
            {
                new MatchupTeam()
                {
                    MemberName = matchup.HomeTeam,
                    MemberId = matchup.HomeMemberID.Value,
                    IsHome = true,
                    IsWinningTeam = matchup.HomeScore > matchup.AwayScore,
                    Score = matchup.HomeScore
                },
                new MatchupTeam()
                {
                    MemberName = matchup.AwayTeam,
                    MemberId = matchup.AwayMemberID.Value,
                    IsHome = false,
                    IsWinningTeam = matchup.HomeScore < matchup.AwayScore,
                    Score = matchup.AwayScore
                },
            }
        };

        public MatchupsDbService(MachoManFFEntities context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Matchup>> GetAllMatchups()
        {
            var matchups = await getMatchupsByPredicateAsync();

            return matchups;
        }

        public async Task<IEnumerable<Matchup>> GetMatchupsForYear(int year)
        {
            var matchups = await getMatchupsByPredicateAsync(m => m.Year == year);

            return matchups;
        }

        public async Task<IEnumerable<Matchup>> GetMatchupsForWeek(int year, int week)
        {
            var matchups = await getMatchupsByPredicateAsync(m => m.Year == year && m.Week == week);

            return matchups;
        }

        public async Task<IEnumerable<TeamSpecificMatchup>> GetMatchupsForYearForTeam(int year, int teamId)
        {
            var dbMatchups = await _context.vHistoricalMatchups
                .Where(s => s.Year == year && (s.AwayMemberID == teamId || s.HomeMemberID == teamId))
                .OrderBy(s => s.Week)
                .ToListAsync();

            var matchups = dbMatchups.Select(db =>
            {
                var isHomeTeam = db.HomeMemberID == teamId;
                var isTie = db.AwayScore == db.HomeScore;
                var m = new TeamSpecificMatchup
                {
                    MatchupId = db.MatchupID,
                    IsHomeTeam = isHomeTeam,
                    IsTie = isTie,
                    IsWinningTeam = (db.HomeScore > db.AwayScore ? isHomeTeam : !isHomeTeam) || isTie,
                    OpponentScore = isHomeTeam ? db.AwayScore : db.HomeScore,
                    OpponentTeam = isHomeTeam ? db.AwayTeam : db.HomeTeam,
                    OpponentTeamId = (isHomeTeam? db.AwayMemberID : db.HomeMemberID).Value,
                    TeamScore = isHomeTeam ? db.HomeScore : db.AwayScore,
                    Week = db.Week
                };

                return m;
            });

            return matchups;
        }

        private async Task<IEnumerable<Matchup>> getMatchupsByPredicateAsync(Expression<Func<vHistoricalMatchup, bool>> predicate = null)
        {
            var query = _context.vHistoricalMatchups.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = (await query.ToListAsync()).Select(_mapper);

            return result;
        }
    }
}
