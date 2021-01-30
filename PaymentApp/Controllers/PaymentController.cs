using Microsoft.AspNetCore.Mvc;
using PaymentManager.Contracts;
using PaymentManager.Requests;
using PaymentManager.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Get payment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponse>> GetPayment(Guid id)
        {
            var paymentDto = await _paymentService.GetPaymentByIdAsync(id);

            if (paymentDto == null)
            {
                return NotFound();
            }

            return paymentDto;
        }

        /// <summary>
        /// Create Payment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> MakePayment(MakePaymentRequest request)
        {
            var payment = await _paymentService.MakePayment(request);

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }
    }
}
