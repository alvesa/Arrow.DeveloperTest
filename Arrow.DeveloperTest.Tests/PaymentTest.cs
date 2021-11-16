using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arrow.DeveloperTest.Tests
{
    //[TestFixture]
    [TestClass]
    public class PaymentTest
    {
        private readonly IPaymentService _paymentService;

        public PaymentTest()
        {
            var host = CreateHostBuilder().Build();

            _paymentService = host.Services.GetRequiredService<IPaymentService>();
        }

        public IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureServices(services => {
                services.AddScoped<IPaymentService, PaymentService>();
                services.AddTransient<IAccountDataStore, AccountDataStore>();
            });
        }

        [TestMethod]
        public void Test_Control_Should_Always_Pass() => Assert.IsTrue(1 == 1);

        [TestMethod]
        public void MakePayment_Should_Return_Object_Valid()
        {
            var makePaymentRequest = new MakePaymentRequest();

            var result = _paymentService.MakePayment(makePaymentRequest);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MakePayment_Should_Return_False_For_Invalid_Account()
        {
            var makePaymentRequest = new MakePaymentRequest();

            var result = _paymentService.MakePayment(makePaymentRequest).Success;

            Assert.IsFalse(result);
        }
    }
}
