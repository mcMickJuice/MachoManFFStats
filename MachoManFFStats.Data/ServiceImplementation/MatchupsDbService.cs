using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<Matchup> GetAllMatchups()
        {
            var matchups = getMatchupsByPredicate();

            return matchups;
        }

        public IEnumerable<Matchup> GetMatchupsForYear(int year)
        {
            var matchups = getMatchupsByPredicate(m => m.Year == year);

            return matchups;
        }

        public IEnumerable<Matchup> GetMatchupsForWeek(int year, int week)
        {
            var matchups = getMatchupsByPredicate(m => m.Year == year && m.Week == week);

            return matchups;
        }

        private IEnumerable<Matchup> getMatchupsByPredicate(Expression<Func<vHistoricalMatchup, bool>> predicate = null)
        {
            var query = _context.vHistoricalMatchups.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = query.ToList().Select(_mapper);

            return result;
        }
    }
}
