using library_management_system.Models;
using library_management_system.Utils;

namespace library_management_system.Data
{
    public static class DbInitializer
    {
        public static void Seed(LibraryDB context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed Books
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Title = "Clean Code", Author = "Robert C. Martin", IsAvailable = true },
                    new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", IsAvailable = true },
                    new Book { Title = "Design Patterns", Author = "Erich Gamma", IsAvailable = true },
                    new Book { Title = "Refactoring", Author = "Martin Fowler", IsAvailable = true },
                    new Book { Title = "You Don’t Know JS", Author = "Kyle Simpson", IsAvailable = true }
                );

                context.SaveChanges(); // Save to generate IDs for Books
            }

            // Seed Members
            if (!context.Members.Any())
            {
                context.Members.AddRange(
                    new Member { Name = "Admin User", Email = "admin@library.com", Role = "admin", Password = Utilities.HashPassword("verysecretpassword") },
                    new Member { Name = "John Doe", Email = "john.doe@example.com", Role = "user", Password = Utilities.HashPassword("verysecretpassword") },
                    new Member { Name = "Jane Smith", Email = "jane.smith@example.com", Role = "user", Password = Utilities.HashPassword("verysecretpassword") },
                    new Member { Name = "Alice Johnson", Email = "alice.johnson@example.com", Role = "user", Password = Utilities.HashPassword("verysecretpassword") }
                );

                context.SaveChanges(); // Save to generate IDs for Members
            }

            // Seed Loans
            if (!context.Loans.Any())
            {
                var books = context.Books.ToList();
                var members = context.Members.ToList();

                context.Loans.AddRange(
                    new Loan
                    {
                        Book = books[0], // Clean Code
                        Member = members[1], // John Doe
                        LoanDate = DateTime.UtcNow.AddDays(-10),
                        ReturnDate = DateTime.UtcNow.AddDays(20),
                        IsExtended = false
                    },
                    new Loan
                    {
                        Book = books[1], // The Pragmatic Programmer
                        Member = members[2], // Jane Smith
                        LoanDate = DateTime.UtcNow.AddDays(-5),
                        ReturnDate = DateTime.UtcNow.AddDays(25),
                        IsExtended = false
                    },
                    new Loan
                    {
                        Book = books[2], // Design Patterns
                        Member = members[3], // Alice Johnson
                        LoanDate = DateTime.UtcNow.AddDays(-15),
                        ReturnDate = DateTime.UtcNow.AddDays(15),
                        IsExtended = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
