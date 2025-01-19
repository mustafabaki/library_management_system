using library_management_system.Services.Loans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loansService;

        public LoansController(ILoansService loansService)
        {
            _loansService = loansService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLoans(Guid userId)
        {
            var loans = await _loansService.GetUserLoans(userId);
            return Ok(loans);
        }

        [HttpPut]
        public async Task<IActionResult> ExtendLoan(Guid loanId, int numberOfDays)
        {
           await _loansService.ExtendLoan(loanId, numberOfDays);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> ReturnLoan(Guid loanId)
        {
            _loansService.ReturnLoan(loanId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoanBook(Guid bookId, Guid memberId)
        {
            _loansService.LoanBook(bookId, memberId);

            return Ok();
        }



    }
}
