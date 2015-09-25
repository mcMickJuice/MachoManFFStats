using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachoManFFStats.BLL.Models;

namespace MachoManFFStats.BLL
{
    public interface IMatchupsService
    {
        Task<IEnumerable<Matchup>> GetAllMatchups();
        Task<IEnumerable<Matchup>> GetMatchupsForYear(int year);
        Task<IEnumerable<Matchup>> GetMatchupsForWeek(int year, int week);
        Task<IEnumerable<TeamSpecificMatchup>> GetMatchupsForYearForTeam(int year, int teamId);
    }
}
