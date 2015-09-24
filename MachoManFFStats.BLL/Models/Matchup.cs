using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachoManFFStats.BLL.Models
{
    public class Matchup
    {
        public int MatchupId { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }
        public bool IsTie { get; set; }
        public IEnumerable<MatchupTeam> Teams { get; set; }
    }

    public class MatchupTeam : Team
    {
        public bool IsHome { get; set; }
        public int Score { get; set; }
        public bool IsWinningTeam { get; set; }
    }
}
