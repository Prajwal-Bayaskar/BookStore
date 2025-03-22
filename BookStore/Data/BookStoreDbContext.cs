using BookStore.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
       

    }
}
