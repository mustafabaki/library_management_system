using library_management_system.Models;

namespace library_management_system.Services.Loans
{
    public interface ILoansService
    {
        public Task<IEnumerable<Loan>> GetUserLoans(Guid userId);
        public Task ExtendLoan(Guid loanId, int numberOfDays);

        public void ReturnLoan(Guid loanId);

        public void LoanBook(Guid bookId, Guid memberId);
    }
}
