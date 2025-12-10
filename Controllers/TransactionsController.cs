using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;
using BankingApi_with_ReactFrontend.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi_with_ReactFrontend.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public TransactionsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> DoTransactionAsync(UpdateAccountFromTransaction NewTransaction)
        {
            var record = await _accountService.DoTransactionAsync(NewTransaction);

            return Ok(record);
        }
    }
}
