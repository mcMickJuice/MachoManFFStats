﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MachoManFFEntities : DbContext
    {
        public MachoManFFEntities()
            : base("name=MachoManFFEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<vHistoricalStanding> vHistoricalStandings { get; set; }
        public virtual DbSet<vAllTimeStanding> vAllTimeStandings { get; set; }
        public virtual DbSet<Standing> Standings { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<vHistoricalMatchup> vHistoricalMatchups { get; set; }
    }
}
