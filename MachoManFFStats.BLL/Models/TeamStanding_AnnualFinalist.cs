namespace MachoManFFStats.BLL.Models
{
    public class TeamStanding_AnnualFinalist : TeamStanding
    {
        public int Year { get; set; }
        public bool MadePlayoffs { get; set; }
        public bool DivisionWinner { get; set; }
        public bool ReceivedBye { get; set; }
        public bool MadeFinals { get; set; }
        public bool IsChampion { get; set; }
    }
}
