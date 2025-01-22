using library_management_system.Data;
using library_management_system.Models;
using library_management_system.Repository;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Services.Loans
{
    public class LoansService : ILoansService
    {
        private readonly IRepository<Loan> _loanRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Member> _memberRepository;
        private readonly LibraryDB _libraryDB;

        public LoansService(IRepository<Member> memberRepository, IRepository<Loan> loanRepository, IRepository<Book> bookRepository, LibraryDB libraryDB)
        {
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _libraryDB = libraryDB;
        }

        public async Task<(bool, Loan)> ExtendLoan(Guid loanId, int numberOfDays)
        {
            var existingRecord = await _loanRepository.GetByIdAsync(loanId);
            if (existingRecord == null || existingRecord.IsExtended == true)
            {
                return (false, new Loan());
            }

            existingRecord.ReturnDate = existingRecord.ReturnDate?.AddDays(numberOfDays);
            existingRecord.IsExtended = true;
            return await _loanRepository.Update(existingRecord);
        }

        public async Task<IEnumerable<Loan>> GetUserLoans(Guid userId)
        {
            return await _libraryDB.Loans
         .Where(loan => loan.Member.Id == userId)
         .ToListAsync();
        }

        public async Task LoanBook(Guid bookId, Guid memberId)
        {
            var book = await _libraryDB.Books.FindAsync(bookId);
            var member = await _libraryDB.Members.FindAsync(memberId);

            book.IsAvailable = false;

            await _libraryDB.Loans.AddAsync(new Loan
            {
                Book = book,
                Member = member,
                IsExtended = false,
                LoanDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(15)
            });

            await _libraryDB.SaveChangesAsync();
        }

        public bool ReturnLoan(Guid loanId)
        {
            var loan = _libraryDB.Loans.Include(x => x.Book).First();
            Book book = loan.Book;

            book.IsAvailable = true;

            _libraryDB.Books.Update(book);

            return _libraryDB.SaveChanges() > 0;

        }
    }
}
