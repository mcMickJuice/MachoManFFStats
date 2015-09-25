using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MachoManFFStats.BLL;
using MachoManFFStats.BLL.Models;

namespace MachoManFFStats.WebApi.Controllers
{
    [RoutePrefix("api/matchup")]
    public class MatchupController : ApiController
    {
        private readonly IMatchupsService _service;

        public MatchupController(IMatchupsService service)
        {
            _service = service;
        }

        //all matchups
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Matchup>> GetAllMatchups()
        {
            var matchups = await _service.GetAllMatchups();

            return matchups;
        }
        //matchups for a given year
        [Route("{year:int}")]
        public async Task<IHttpActionResult> GetMatchups_ForYear(int year)
        {
            var matchups = await _service.GetMatchupsForYear(year);

            if (!matchups.Any())
            {
                return NotFound();
            }

            return Ok(matchups);
        }

        //matchups for a given week
        [Route("{year:int}/{week:int}")]
        public async Task<IHttpActionResult> GetMatchups_ForWeek(int year, int week)
        {
            var matchups = await _service.GetMatchupsForWeek(year, week);

            if (!matchups.Any())
            {
                return NotFound();
            }

            return Ok(matchups);
        }

        [Route("{year:int}/team/{teamId:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMatchups_ForYear_ForTeam(int year, int teamId)
        {
            var matchups = await _service.GetMatchupsForYearForTeam(year, teamId);

            if (!matchups.Any())
            {
                return NotFound();
            }

            return Ok(matchups);
        }
    }
}