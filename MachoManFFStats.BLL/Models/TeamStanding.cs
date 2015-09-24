namespace MachoManFFStats.BLL.Models
{
    public class Team
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
    }

    public class TeamStanding : Team
    {
        public int PointsScored { get; set; }
        public int PointsAgainst { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        private decimal _winPercentage;

        public decimal WinPercentage
        {
            get { return decimal.Round(_winPercentage * 100,2);}
            set { _winPercentage = value; }
        }
    }
}