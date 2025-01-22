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
            var (result, data) = await _loansService.ExtendLoan(loanId, numberOfDays);

            if (result)
            {
                return Ok(data);
            }

            return BadRequest(new { message = "Your loan could not be extended." });
        }


        [HttpPut]
        public async Task<IActionResult> ReturnLoan(Guid loanId)
        {
            var result = _loansService.ReturnLoan(loanId);

            if (result)
            {
                return Ok(new { message = "The book has been returned successfully."});
            }

            return BadRequest(new { message = "Oops! Something went wrong!" });
        }

        [HttpPost]
        public async Task<IActionResult> LoanBook(Guid bookId, Guid memberId)
        {
            await _loansService.LoanBook(bookId, memberId);

            return Ok();
        }



    }
}
