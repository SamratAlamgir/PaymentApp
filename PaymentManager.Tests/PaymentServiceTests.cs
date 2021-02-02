using BankApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaymentManager.Contracts;
using PaymentManager.Requests;
using PaymentManager.Responses;
using PaymentManager.Validators;
using Repository.Models;
using Repository.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PaymentManager.Tests
{
    [TestClass]
    public class PaymentServiceTests
    {
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private Mock<IMerchantService> _merchantServiceMock;
        private Mock<IBankService> _bankServiceMock;
        private CardValidatorProvider _cardValidatorProvider;
        private BankPaymentResponse bankPaymentResponse;

        private PaymentService paymentService;
        private Payment payment;

        [TestInitialize()]
        public void Initialize()
        {
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _merchantServiceMock = new Mock<IMerchantService>();
            _bankServiceMock = new Mock<IBankService>();
            _cardValidatorProvider = new CardValidatorProvider();

            _merchantServiceMock.Setup(m => m.GetMerchantById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(
                    new MerchantResponse
                    {
                        MerchantId = Guid.NewGuid()
                    }));

            bankPaymentResponse = new BankPaymentResponse
            {
                BankPaymentRef = Guid.NewGuid(),
                PaymentStatus = AppCodes.PaymentStatus.Successful

            };

            _bankServiceMock.Setup(b => b.MakeBankPayment(It.IsAny<BankPaymentRequest>()))
                .Returns(Task.FromResult( bankPaymentResponse));

            payment = new Payment
            {
                BankPaymentRef = bankPaymentResponse.BankPaymentRef,
                PaymentStaus = (short)bankPaymentResponse.PaymentStatus
            };

            _paymentRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Payment>()))
                .Returns(Task.FromResult(payment));

            paymentService = new PaymentService(
                _paymentRepositoryMock.Object,
                _merchantServiceMock.Object,
                _bankServiceMock.Object,
                _cardValidatorProvider);
        }

        [TestMethod]
        public async Task MakePayment_WithValidRequest_MappedResponseProperly()
        {
            var paymentRequest = new MakePaymentRequest
            {
                CardNumber = "1245678974558"
            };

            PaymentResponse response = await paymentService.MakePayment(paymentRequest);

            Assert.IsNotNull(response);
            Assert.AreEqual(bankPaymentResponse.BankPaymentRef, response.BankPaymentRef);
            Assert.AreEqual(bankPaymentResponse.PaymentStatus.ToString(), response.PaymentStaus);
            Assert.AreEqual(bankPaymentResponse.PaymentStatus.ToString(), response.PaymentStaus);
        }

        [TestMethod]
        public async Task MakePayment_WithValidRequest_PaymentSavedOnce()
        {
            var paymentRequest = new MakePaymentRequest
            {
                CardNumber = "2245678974558"
            };

            PaymentResponse response = await paymentService.MakePayment(paymentRequest);

            Assert.IsNotNull(response);

            _bankServiceMock.Verify(b => b.MakeBankPayment(It.IsAny<BankPaymentRequest>()), Times.Once);
            _paymentRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Payment>()), Times.Once);
        }

        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public async Task MakePayment_InvalidCardNumber_ThrowsValidationException()
        {
            var paymentRequest = new MakePaymentRequest
            {
                CardNumber = "abc2245678974558"
            };

            await paymentService.MakePayment(paymentRequest);
        }
    }
}
