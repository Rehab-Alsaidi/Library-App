using Book_Recommendation_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_Recommendation_System.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Book { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.FavoriteBooks)
                .WithMany(b => b.UsersWhoMarkedAsFavorite)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserFavoriteBooks",
                    j => j.HasOne<Book>().WithMany().HasForeignKey("ISBN"),
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "ISBN");
                        j.ToTable("AspNetUserFavoriteBooks");
                    }
                );
        }






    }
}
