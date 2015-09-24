using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachoManFFStats.BLL;
using MachoManFFStats.BLL.Models;
using MachoManFFStats.Data.Entity;

namespace MachoManFFStats.Data.ServiceImplementation
{
    public class StandingsDbService : IStandingsService
    {
        private MachoManFFEntities _context;
        private Func<vHistoricalStanding, TeamStanding> _mapper = standing => new TeamStanding()
        {
            Losses = standing.Losses.Value,
            Wins = standing.Wins.Value,
            MemberId = standing.MemberID,
            MemberName = standing.MemberName,
            PointsAgainst = standing.PointsAgainst.Value,
            PointsScored = standing.PointsScored.Value,
            WinPercentage = standing.WinPercentage.Value
        };

        public StandingsDbService(MachoManFFEntities context)
        {
            _context = context;
        }

        public IEnumerable<TeamStanding> GetAllTimeStandings()
        {
            var standings = _context.vAllTimeStandings.ToList();

            var mapped = standings.Select(s => new TeamStanding
            {
                Losses = s.Losses.Value,
                Wins = s.Wins.Value,
                MemberId = s.MemberId,
                MemberName = s.MemberName,
                PointsAgainst = s.PointsAgainst.Value,
                PointsScored = s.PointsScored.Value,
                WinPercentage = s.WinPercentage.Value
            });

            return mapped;
        }

        public IEnumerable<TeamStanding_AnnualFinalist> GetStandings_ForYear(int year)
        {
            var mappedStandings = _context.vHistoricalStandings
                .Where(s => s.Year == year)
                .ToList()
                .Select(s =>
                {
                    var standing = new TeamStanding_AnnualFinalist
                    {
                        Losses = s.Losses.Value,
                        Wins = s.Wins.Value,
                        MemberId = s.MemberID,
                        MemberName = s.MemberName,
                        PointsAgainst = s.PointsAgainst.Value,
                        PointsScored = s.PointsScored.Value,
                        WinPercentage = s.WinPercentage.Value,
                        DivisionWinner = s.DivisionWinner,
                        IsChampion = s.IsChampion,
                        MadePlayoffs = s.MadePlayoffs,
                        MadeFinals = s.ReachedFinals,
                        ReceivedBye = s.ReceivedBye,
                    };

                    return standing;
                });

            return mappedStandings;
        }

        public IEnumerable<AnnualLeagueStanding> GetGroupedStandings_ByYear()
        {
            var mappedStandings = _context.vHistoricalStandings
                .GroupBy(s => s.Year)
                .ToList()
                .Select(g => new AnnualLeagueStanding
                {
                    Year = g.Key,
                    TeamStanding = g.Select(_mapper)
                });

            return mappedStandings;
        }

        public IEnumerable<AllTimeFinalistStanding> GetLeagueFinalists_AllTime()
        {
            var standings = _context.Standings.GroupBy(s => new {s.Member.MemberName, s.MemberID})
                .Select(g => new AllTimeFinalistStanding
                {
                    MemberId = g.Key.MemberID,
                    MemberName = g.Key.MemberName,
                    Byes = g.Count(s => s.ReceivedBye),
                    DivisionChampionships = g.Count(s => s.DivisionWinner),
                    FinalsAppearances = g.Count(s => s.ReachedFinals),
                    TotalChampionships = g.Count(s => s.IsChampion),
                    PlayoffsAppearances = g.Count(s => s.MadePlayoffs),
                    NumberOfSeasons = g.Count()
                })
                .ToList();

            return standings;
        }

        public void GetGroupedLeagueFinalists_ByYear()
        {
            throw new NotImplementedException();
        }

        //        public void GetLeagueFinalists_ForYear(int year)
        //        {
        //            var finalists = _context.vHistoricalStandings
        //                .Where(s => s.Year == year)
        //                .ToList()
        //                .Select();
        //        }
    }
}
