using System.Collections.Generic;
using System.Web.Http;
using MachoManFFStats.BLL;
using MachoManFFStats.BLL.Models;
using Microsoft.Ajax.Utilities;

namespace MachoManFFStats.WebApi.Controllers
{
    [RoutePrefix("api/standing")]
    public class LeagueStandingController : ApiController
    {
        private readonly IStandingsService _standingService;

        public LeagueStandingController(IStandingsService standingService)
        {
            _standingService = standingService;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<TeamStanding> GetHistoricalStandings()
        {
            var standings = _standingService.GetAllTimeStandings();

            return standings;
        }

        [Route("{year:int}")]
        [HttpGet]
        public IEnumerable<TeamStanding_AnnualFinalist> GetStandingsForYear(int year)
        {
            var standings = _standingService.GetStandings_ForYear(year);
            return standings;
        }

        [Route("grouped")]
        [HttpGet]
        public IEnumerable<AnnualLeagueStanding> GetGroupedStandings()
        {
            var groupedStandings = _standingService.GetGroupedStandings_ByYear();

            return groupedStandings;
        }
//
//        [Route("finalists/grouped")]
//        public void GetLeagueFinalistsGrouped()
//        {
//            var grouped = _standingService.GetGroupedLeagueFinalists_ByYear();
//
//            return grouped;
//        }

        [Route("finalists/alltime")]
        [HttpGet]
        public IEnumerable<AllTimeFinalistStanding> GetLeagueFinalistsAllTime()
        {
            var standings = _standingService.GetLeagueFinalists_AllTime();
            return standings;
        }
    }
}
