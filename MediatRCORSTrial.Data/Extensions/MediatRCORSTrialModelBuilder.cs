using MediatRCORSTrial.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRCORSTrial.Data.Extensions
{
    public static class MediatRCORSTrialModelBuilder
    {
        public static void MediatRCORSTrialModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p => {
                p.HasKey(pr => pr.Id);
                p.ToTable("Products");
                p.Property(prop => prop.Name).HasMaxLength(20);
            });
            modelBuilder.Entity<Category>(p => {
                p.HasKey(pr => pr.Id);
                p.ToTable("Category");
                p.Property(prop => prop.Name).HasMaxLength(20);
            });
            modelBuilder.Entity<User>(p => {
                p.HasKey(pr => pr.Id);
                p.ToTable("User");
                p.Property(prop => prop.Username).HasMaxLength(20);
            });

        }
    }
}
