using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachoManFFStats.BLL.Models
{
    public class AnnualLeagueStanding
    {
        public int Year { get; set; }
        public IEnumerable<TeamStanding> TeamStanding { get; set; }
    }
}
