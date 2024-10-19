using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Book)
                .WithMany(b => b.Wishlists)
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<UserReview>()
                .HasOne(ur => ur.Sender)
                .WithMany(u => u.SentReviews)
                .HasForeignKey(ur => ur.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserReview>()
                .HasOne(ur => ur.Recipient)
                .WithMany(u => u.ReceivedReviews)
                .HasForeignKey(ur => ur.RecipientId);
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.Genres)
                .HasForeignKey(bg => bg.BookId);
            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(bg => bg.GenreId);
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(ba => ba.AuthorId);
            modelBuilder.Entity<Chat>()
               .HasKey(cr => cr.Id);

            modelBuilder.Entity<Message>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.UserOne)
                .WithMany()
                .HasForeignKey(c => c.UserOneId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.UserTwo)
                .WithMany()
                .HasForeignKey(c => c.UserTwoId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Book)
                .WithMany(b => b.Wishlists)
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
