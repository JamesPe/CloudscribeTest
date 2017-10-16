using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ESDM.Models;

namespace ESDM.Models
{
    public partial class CMSiDataContext : DbContext
    {
        public virtual DbSet<Site> Sites { get; set; }

        //public virtual DbSet<SitesListViewModel> SitesListViewModel { get; set; }

        public CMSiDataContext(DbContextOptions<CMSiDataContext> options) : base(options)
        { }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=SQL2008;Database=CMSI-NE-DSites-Marine;User ID=cmsine;Password=cmsine;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>(entity => { entity.HasKey(e => e.SiteCode); });
            //modelBuilder.Entity<SitesListViewModel>(entity => { entity.HasKey(e => e.SiteCode); });

            {
                modelBuilder.Entity<Site>().ToTable("vwSiteSearchTest", "CES");
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Site>(entity => { entity.HasKey(e => e.SiteCode); });
            }
        }
    }
}




