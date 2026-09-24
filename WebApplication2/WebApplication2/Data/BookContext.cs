using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data;

public class BookContext : DbContext
{
    // This constructor receives the settings (connection string, provider)
    // from the DI container. We pass them straight up to the base class.
    // Without it, EF Core would have no idea WHERE to connect.
    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }
    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");

            entity.HasKey(b => b.Id);

            entity.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(200);
            
            // for the "create if it doesn't exist yet" rule.
            
            entity.HasIndex(b => b.Title).IsUnique();
            
            entity.HasOne(b => b.Owner)              // each book has ONE owner...
                .WithMany(u => u.Books)            // ...and each user has MANY books
                .HasForeignKey(b => b.OwnerId)     // the sticker column is OwnerId
                .OnDelete(DeleteBehavior.Restrict); 
            
        });
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

    }
}