using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Arrow.DeveloperTest.Tests
{
    public class PaymentTest
    {
        private readonly IPaymentService _paymentService;

        public PaymentTest()
        {
            var service = new ServiceCollection();
            service.AddTransient<IPaymentService, PaymentService>();
            service.AddTransient<IAccountDataStore, AccountDataStore>();

            var provider = service.BuildServiceProvider();
            _paymentService = provider.GetService<IPaymentService>();
        }

        
        [Fact]
        public void MakePayment_Should_Return_Object_Valid()
        {
            var makePaymentRequest = new MakePaymentRequest();

            var result = _paymentService.MakePayment(makePaymentRequest);

            Assert.IsNotNull(result);
        }

        [Fact]
        public void MakePayment_Should_Return_True_For_Valid_Acount()
        {
            var makePaymentRequest = new MakePaymentRequest();

            var result = _paymentService.MakePayment(makePaymentRequest).Success;

            Assert.IsTrue(result);
        }
    }
}
