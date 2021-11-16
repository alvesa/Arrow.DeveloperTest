using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Data;

namespace Arrow.DeveloperTest.Runner
{
    public static class Startup
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(services => {
                services.AddScoped<Program>();
                services.AddScoped<IPaymentService, PaymentService>();
                services.AddTransient<IAccountDataStore, AccountDataStore>();
            });
        }
    }
}
