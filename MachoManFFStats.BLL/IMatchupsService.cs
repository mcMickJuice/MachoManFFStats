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
        IEnumerable<Matchup> GetAllMatchups();
        IEnumerable<Matchup> GetMatchupsForYear(int year);
        IEnumerable<Matchup> GetMatchupsForWeek(int year, int week);
    }
}
