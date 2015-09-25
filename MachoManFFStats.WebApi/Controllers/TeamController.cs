using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MachoManFFStats.BLL;
using MachoManFFStats.BLL.Models;

namespace MachoManFFStats.WebApi.Controllers
{
    [RoutePrefix("api/team")]
    public class TeamController : ApiController
    {
        private readonly ITeamsService _service;

        public TeamController(ITeamsService service)
        {
            _service = service;
        }

        //get all teams
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Team>>  GetAllTeams()
        {
            var teams = await _service.GetAllTeams();

            return teams;
        }
        //get active teams
        [Route("active")]
        [HttpGet]
        public async Task<IEnumerable<Team>>  GetActiveTeams()
        {
            var teams = await _service.GetActiveTeams();

            return teams;
        }

        [Route("season/{year:int}")]
        [HttpGet]
//        public async Task<IEnumerable<FfTeam>>  GetTeams_ForYear(int year)
        public async Task<IHttpActionResult>  GetTeams_ForYear(int year)
        {
            var teams = await _service.GetFantasyTeams_ForYear(year);

            if (!teams.Any())
            {
                return NotFound();
            }

            return Ok(teams);
        }

        //team by id
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetTeamByMemberId(int id)
        {
            var team = await _service.GetTeamProfileById(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }
    }
}
