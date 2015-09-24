using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachoManFFStats.BLL.Models;

namespace MachoManFFStats.BLL
{
    public interface IStandingsService
    {
        IEnumerable<TeamStanding> GetAllTimeStandings();
        IEnumerable<TeamStanding_AnnualFinalist> GetStandings_ForYear(int year);
        IEnumerable<AnnualLeagueStanding> GetGroupedStandings_ByYear();
        IEnumerable<AllTimeFinalistStanding> GetLeagueFinalists_AllTime();
        void GetGroupedLeagueFinalists_ByYear();
//        void GetLeagueFinalists_ForYear(int year);
    }
}
