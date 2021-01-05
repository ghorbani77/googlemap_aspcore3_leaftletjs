using Darayas.Maps.DAL.Models;
using Darayas.Maps.DAL.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darayas.Maps.DAL
{
    public class MainContext : IdentityDbContext<tblUsers>
    {
        public MainContext(DbContextOptions<MainContext> options)
           : base(options)
        {
        }

        public MainContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(new ConnStr().GetConn());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region tblPalceCategoris
            builder.Entity<tblPalceCategoris>().HasKey(a => a.Id);
            builder.Entity<tblPalceCategoris>().Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Entity<tblPalceCategoris>().Property(a => a.ImgName).IsRequired().HasMaxLength(150);
            builder.Entity<tblPalceCategoris>().Property(a => a.Title).IsRequired().HasMaxLength(150);
            #endregion

            #region tblPalces
            builder.Entity<tblPalces>().HasKey(a => a.Id);
            builder.Entity<tblPalces>().Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Entity<tblPalces>().Property(a => a.CateId).IsRequired().HasMaxLength(150);
            builder.Entity<tblPalces>().Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.Entity<tblPalces>()
                .HasOne(a => a.tblPalceCategoris)
                .WithMany(a => a.tblPalces)
                .HasForeignKey(a => a.CateId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            base.OnModelCreating(builder);
        }


        public DbSet<tblPalces> tblPalces { get; set; }
        public DbSet<tblPalceCategoris> tblPalceCategoris { get; set; }
    }
}
