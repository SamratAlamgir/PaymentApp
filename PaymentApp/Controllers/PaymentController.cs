using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentApp.Attributes;
using PaymentManager.Contracts;
using PaymentManager.Requests;
using PaymentManager.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Controllers
{
    [Produces("application/json")]
    [Route("api/payments")]
    [ApiController]
    [ApiKeyAuthorize]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Get payment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// /// <response code="200">Returns payment for the Id</response>
        /// <response code="404">If the payment not found</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponse>> GetPayment(Guid id)
        {
            _logger.LogInformation($"GetPayment invoked with id {id}");

            var paymentDto = await _paymentService.GetPaymentByIdAsync(id);

            if (paymentDto == null)
            {
                _logger.LogError($"Payment info not found for id {id}");
                return NotFound();
            }

            return paymentDto;
        }

        /// <summary>
        /// Create Payment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created Payment information</returns>
        /// <response code="201">Returns the newly created payment</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> MakePayment(MakePaymentRequest request)
        {
            _logger.LogInformation("New payment requested");

            var payment = await _paymentService.MakePayment(request);

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }
    }
}
