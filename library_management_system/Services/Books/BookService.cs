using library_management_system.Models;
using library_management_system.Repository;

namespace library_management_system.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public void DeleteBook(Guid id)
        {
            var book = _bookRepository.GetByIdAsync(id).Result;
            _bookRepository.Delete(book);
        }

        public async Task<(IEnumerable<Book>, int)> SearchBooksAsync(string title, string author, int page, int pageSize)
        {
            return await _bookRepository.GetPagedAsync(
                query =>
                {
                    if (!string.IsNullOrEmpty(title))
                    {
                        query = query.Where(b => b.Title.Contains(title));
                    }

                    if (!string.IsNullOrEmpty(author))
                    {
                        query = query.Where(b => b.Author.Contains(author));
                    }

                    return query.OrderBy(b => b.Title);
                },
                page,
                pageSize
            );
        }

    }
}
