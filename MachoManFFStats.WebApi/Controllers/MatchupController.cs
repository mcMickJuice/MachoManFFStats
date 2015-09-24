using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IEnumerable<Matchup> GetAllMatchups()
        {
            var matchups = _service.GetAllMatchups();

            return matchups;
        }
        //matchups for a given year
        [Route("{year:int}")]
        public IEnumerable<Matchup> GetMatchups_ForYear(int year)
        {
            var matchups = _service.GetMatchupsForYear(year);

            return matchups;
        }

        //matchups for a given week
        [Route("{year:int}/{week:int}")]
        public IEnumerable<Matchup> GetMatchups_ForWeek(int year, int week)
        {
            var matchups = _service.GetMatchupsForWeek(year, week);

            return matchups;
        }
        
    }
}