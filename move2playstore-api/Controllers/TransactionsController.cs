using Microsoft.AspNetCore.Mvc;
using move2playstoreAPI.DataTransferObjects;
using move2playstoreAPI.Models;
using System;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace move2playstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly Move2PlayStoreDBContext _context;

        public TransactionsController(Move2PlayStoreDBContext context)
        {
            _context = context;
        }

        // POST: api/Transactions
        [HttpPost]
        public IActionResult PostTransactions([FromBody] TransactionDto transaction)
        {
            if (transaction?.UserId == null || transaction.Itens == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                const bool isSandbox = true;
                EnvironmentConfiguration.ChangeEnvironment(isSandbox);

                var credentials = PagSeguroConfiguration.Credentials(isSandbox);
                var payment = new PaymentRequest { Currency = Currency.Brl };
                var id = 1;

                foreach (var item in transaction.Itens)
                {
                    var paddedId = id.ToString().PadLeft(4, '0');
                    payment.Items.Add(new Item(paddedId, item.ItemDescription, item.ItemQuantity, (decimal)item.ItemAmount));
                    id += 1;
                }

                payment.RedirectUri = new Uri("http://localhost:3000/carrinho");

                var paymentRedirectUri = payment.Register(credentials);

                var code = paymentRedirectUri.ToString().Substring(paymentRedirectUri.ToString().IndexOf("code=", StringComparison.Ordinal)).Remove(0, 5);

                SaveTransaction(transaction, code);

                return Ok(paymentRedirectUri);
            }
            catch (PagSeguroServiceException exception)
            {
                return StatusCode(500, exception.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Transactions
        [HttpGet("{code}")]
        public IActionResult CheckTransaction([FromRoute] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }
            try
            {
                const bool isSandbox = true;
                EnvironmentConfiguration.ChangeEnvironment(isSandbox);
                var credentials = PagSeguroConfiguration.Credentials(isSandbox);
                var transaction = TransactionSearchService.SearchByCode(credentials, code, false);
                return Ok(transaction);
            }
            catch (PagSeguroServiceException exception)
            {
                return StatusCode(500, exception.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Transactions/Notification/{F5F6041F-DB5D-47D6-8185-3A994605EBD5}
        [HttpGet("Notification/{code}")]
        public IActionResult CheckNotification([FromRoute] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }
            try
            {
                const bool isSandbox = true;
                EnvironmentConfiguration.ChangeEnvironment(isSandbox);
                var credentials = PagSeguroConfiguration.Credentials(isSandbox);
                var notification = NotificationService.CheckTransaction(credentials, code, false);
                return Ok(notification);
            }
            catch (PagSeguroServiceException exception)
            {
                return StatusCode(500, exception.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void SaveTransaction(TransactionDto transaction, string code)
        {
            var purchase = new Purchase
            {
                UserId = transaction.UserId,
                PurchaseDate = DateTime.Now,
                PaymentMethod = 1,
                PaymentStatus = 0,
                PaymentStatusMessage = "NOT PAYED",
                PaymentToken = code,
            };

            _context.Purchase.Add(purchase);
            _context.SaveChanges();

            foreach (var item in transaction.Itens)
            {
                var purchaseItem = new Purchaseitem
                {
                    GameId = item.GameId,
                    Price = item.ItemAmount,
                    PurchaseId = purchase.Id
                };
                _context.Purchaseitem.Add(purchaseItem);
            }

            _context.SaveChanges();
        }
    }
}