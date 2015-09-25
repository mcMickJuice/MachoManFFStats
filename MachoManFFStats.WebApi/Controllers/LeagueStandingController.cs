using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<TeamStanding>> GetHistoricalStandings()
        {
            var standings = await _standingService.GetAllTimeStandings();

            return standings;
        }

        [Route("{year:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStandingsForYear(int year)
        {
            var standings = await _standingService.GetStandings_ForYear(year);

            if (!standings.Any())
            {
                return NotFound();
            }

            return Ok(standings);
        }

        [Route("grouped")]
        [HttpGet]
        public async Task<IEnumerable<AnnualLeagueStanding>> GetGroupedStandings()
        {
            var groupedStandings = await _standingService.GetGroupedStandings_ByYear();

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
        public async Task<IEnumerable<AllTimeFinalistStanding>> GetLeagueFinalistsAllTime()
        {
            var standings = await _standingService.GetLeagueFinalists_AllTime();
            return standings;
        }
    }
}
