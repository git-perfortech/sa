//using Microsoft.EntityFrameworkCore;
using MediatRCORSTrial.Core.Common.Contracts;
using MediatRCORSTrial.Core.Enums;
using MediatRCORSTrial.Data.Entities;
using MediatRCORSTrial.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Data.Context
{
    public class MediatRCORSTrialContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public MediatRCORSTrialContext()
        {
        }

        public MediatRCORSTrialContext(DbContextOptions<MediatRCORSTrialContext> options)
           : base(options)
        {
        }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Product>()
        //        .HasRequired(p => p.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(p => p.CategoryId);

        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.AllowedCategories)
        //        .WithMany(c => c.AssignedUsers)
        //        .Map(mapping =>
        //        {
        //            mapping.MapLeftKey("UserId");
        //            mapping.MapRightKey("CategoryId");
        //            mapping.ToTable("UserCategory");
        //        });
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");

            if (!optionsBuilder.IsConfigured)
            {

                ConnectionType domainContext = GetConnectionType.Get("MediatRCORSTrialContext");

                switch (domainContext.DbType)
                {
                    case DatabaseProviders.SqlServer:
                        optionsBuilder.UseSqlServer(domainContext.ConnectionString);
                        break;

                    case DatabaseProviders.Oracle:
                        optionsBuilder.UseOracle(domainContext.ConnectionString);
                        break;

                    case DatabaseProviders.PostgreSql:
                        optionsBuilder.UseNpgsql(domainContext.ConnectionString);
                        break;

                    case DatabaseProviders.MySql:
                        optionsBuilder.UseMySQL(domainContext.ConnectionString);
                        break;

                    case DatabaseProviders.Sqlite:
                        optionsBuilder.UseSqlite(domainContext.ConnectionString);
                        break;

                    default:
                        break;
                }
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.MediatRCORSTrialModel();
        }

    }
}
