using BankingApi_with_ReactFrontend.Server.Models;
using BankingApi_with_ReactFrontend.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi_with_ReactFrontend.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync(CreateAccount createAccount)
        {
            var NewAcctId = await _accountService.CreateAccountAsync(createAccount);

            return Created(string.Empty, NewAcctId);
        }

        [HttpGet("byId")] 
        public async Task<IActionResult> GetBankAccountAsync([FromQuery] Guid AcctId)
        {
            var record = await _accountService.GetBankAccountAsync(AcctId);

            return Ok(record);
        }

        [HttpGet("Transactions")]
        public async Task<IActionResult> GetTransactionHistoryAsync([FromQuery] TransactionHistoryObject transactionHistoryObject,[FromQuery] Guid AcctId)
        {
            var transactionHistory = await _accountService.GetTransactionHistoryAsync(transactionHistoryObject, AcctId);

            return Ok(transactionHistory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBankAccountsAsync()
        {
            var bankAccounts = await _accountService.GetAllBankAccountsAsync();

            return Ok(bankAccounts);
        }
    }
}
