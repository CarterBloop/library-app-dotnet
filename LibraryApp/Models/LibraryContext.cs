using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }

    // Override OnModelCreating to configure the many-to-many relationship
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.bookId, ba.authorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.bookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.authorId);
    }
}
