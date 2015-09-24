using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachoManFFStats.BLL.Models
{
    public class AllTimeFinalistStanding : Team
    {
        public int TotalChampionships { get; set; }
        public int FinalsAppearances { get; set; }
        public int PlayoffsAppearances { get; set; }
        public int Byes { get; set; }
        public int DivisionChampionships { get; set; }
        public int NumberOfSeasons { get; set; }
    }
}
