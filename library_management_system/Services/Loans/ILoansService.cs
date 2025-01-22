using library_management_system.Models;

namespace library_management_system.Services.Loans
{
    public interface ILoansService
    {
        public Task<IEnumerable<Loan>> GetUserLoans(Guid userId);
        public Task<(bool, Loan)> ExtendLoan(Guid loanId, int numberOfDays);

        public bool ReturnLoan(Guid loanId);

        public Task LoanBook(Guid bookId, Guid memberId);
    }
}
