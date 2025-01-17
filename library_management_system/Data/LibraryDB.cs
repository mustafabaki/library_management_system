using library_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Data
{
    public class LibraryDB : DbContext

    {
        public LibraryDB(DbContextOptions<LibraryDB> options): base(options) { }

        public DbSet<Book> Books{ get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DbSet<Member> Members { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
