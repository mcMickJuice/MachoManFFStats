using System.Collections.Generic;

namespace MachoManFFStats.BLL.Models
{
    public class TeamProfile : Team
    {
        public IEnumerable<FfTeam> FantasyTeams { get; set; } 
    }

    public class FfTeam
    {
        public string TeamName { get; set; }
        public int Year { get; set; }
        public int EspnTeamId { get; set; }
        //public TeamStanding Standing { get; set; }
    }
}
