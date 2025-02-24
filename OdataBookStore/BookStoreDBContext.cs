using Microsoft.EntityFrameworkCore;
using OdataBookStore.entity;

namespace OdataBookStore
{
    public class BookStoreDBContext :DbContext 
    {
        public BookStoreDBContext() { } 

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options) { }

        public virtual DbSet<Book> Books { get; set; }  
        public virtual DbSet<Author> Author { get; set; }   
        public virtual DbSet<BookAuthor> BookAuthor { get; set; }   
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                 .HasOne(u => u.Role)
                 .WithMany(r => r.Users)
                 .HasForeignKey(u => u.RoleId)
                 .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.Publisher)
                .WithMany(p => p.Users)
                .HasForeignKey(t => t.PubId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
               .HasOne(b => b.Publisher)
               .WithMany(p => p.Books)
               .HasForeignKey(b => b.PubId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookAuthor>()
               .HasKey(x => new { x.AuthorId, x.BookId });


            // Seed data for Role
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleDesc = "admin" },
                new Role { RoleId = 2, RoleDesc = "user" }
            );

            // Seed data for Publishers
            modelBuilder.Entity<Publisher>().HasData(
                 new Publisher { PubId = 1, PubName = "tác giả 2", City = "binh dinh", State = "vn", Country = "viet nam " },
                new Publisher { PubId = 2, PubName = "tác giả 1", City ="binh dinh", State = "vn", Country = "viet nam " }
            );

            // Seed data for Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@example.com" },
                new Author { AuthorId = 2, FirstName = "Jane", LastName = "Smith", EmailAddress = "jane.smith@example.com" }
            );

            // Seed data for Books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "C# Programming", Type = 1, PubId = 1, Price = 29.99, PublishedDate = new DateTime(2022, 1, 1) },
                new Book { BookId = 2, Title = "ASP.NET Core", Type = 2, PubId = 2, Price = 39.99, PublishedDate = new DateTime(2023, 5, 15) }
            );

            // Seed data for BookAuthor
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { AuthorId = 1, BookId = 1, AuthorOrder = "1", RoyalityPercentage = 15.5 },
                new BookAuthor { AuthorId = 2, BookId = 2, AuthorOrder = "1", RoyalityPercentage = 12.5 }
            );

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, EmailAddress = "tinh@example.com", Password = "admin123", FirstName = "Admin", LastName = "User", RoleId = 1, PubId = 1, HireDate = new DateTime(2021, 6, 1) },
                new User { UserId = 2, EmailAddress = "tinhuser@example.com", Password = "user123", FirstName = "Regular", LastName = "User", RoleId = 2, PubId = 2, HireDate = new DateTime(2022, 8, 10) }
            );






        }


    }
}
