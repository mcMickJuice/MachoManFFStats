using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachoManFFStats.BLL.Models;

namespace MachoManFFStats.BLL
{
    public interface ITeamsService
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<IEnumerable<Team>> GetActiveTeams();
        Task<IEnumerable<FfTeam>> GetFantasyTeams_ForYear(int year);
        Task<TeamProfile> GetTeamProfileById(int id);
    }
}
