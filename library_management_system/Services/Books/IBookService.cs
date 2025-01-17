using library_management_system.Models;

namespace library_management_system.Services.Books
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task AddBookAsync(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Guid id);

        Task<(IEnumerable<Book>, int)> SearchBooksAsync(string title, string author, int page, int pageSize);
    }

}
