//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MachoManFFStats.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class FFTeam
    {
        public int FFTeamID { get; set; }
        public string Team { get; set; }
        public int ESPNFFTeamID { get; set; }
        public Nullable<int> MemberID { get; set; }
        public int Year { get; set; }
    
        public virtual Member Member { get; set; }
    }
}
