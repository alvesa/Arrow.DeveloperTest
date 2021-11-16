using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Arrow.DeveloperTest.Runner
{
    class Program
    {
        private readonly IPaymentService _paymentService;

        public Program(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        static void Main(string[] args)
        {
            var host = Startup.CreateHostBuilder(args).Build();
            try
            {
                host.Services.GetRequiredService<Program>().Run();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Run()
        {
            Console.WriteLine("Starting the payment.");

            // >>> Enter Here the correct data
            var paymentResult = _paymentService.MakePayment(new MakePaymentRequest());

            if (!paymentResult.Success)
                throw new Exception("Payment not done successfully.");
            else
                Console.WriteLine("Payment done.");

            Console.WriteLine("End.");
        }
    }
}
