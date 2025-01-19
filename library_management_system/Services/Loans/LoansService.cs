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

        public async Task ExtendLoan(Guid loanId, int numberOfDays)
        {
            var existingRecord = await _loanRepository.GetByIdAsync(loanId);
            if (existingRecord == null || existingRecord.IsExtended == true)
            {
                return;
            }

            existingRecord.ReturnDate = existingRecord.ReturnDate?.AddDays(numberOfDays);
            existingRecord.IsExtended = true;
            _loanRepository.Update(existingRecord);
        }

        public async Task<IEnumerable<Loan>> GetUserLoans(Guid userId)
        {
            return await _libraryDB.Loans
         .Where(loan => loan.Member.Id == userId)
         .ToListAsync();
        }

        public async void LoanBook(Guid bookId, Guid memberId)
        {
            Member member = await _memberRepository.GetByIdAsync(memberId);
            Book book = await _bookRepository.GetByIdAsync(bookId);
            book.IsAvailable = false;
            
            Loan loan = new Loan();
            loan.Member = member;
            loan.Book = book;
            loan.Member = member;
            loan.LoanDate = DateTime.UtcNow;
            loan.ReturnDate = DateTime.UtcNow.AddDays(20);
            loan.IsExtended = false;
            
            await _loanRepository.AddAsync(loan);
            
        }

        public async void ReturnLoan(Guid loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
            {
                return;
            }

            Book bookToReturn = loan.Book;
            
            bookToReturn.IsAvailable = true;

            _bookRepository.Update(bookToReturn);

        }
    }
}
