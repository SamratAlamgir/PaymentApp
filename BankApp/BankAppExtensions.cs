using Microsoft.Extensions.DependencyInjection;

namespace BankApp
{
    public static class BankAppExtensions
    {
        public static void RegisterBankAppTypes(this IServiceCollection services)
        {
            services.AddSingleton<IBankService, BankServiceMock>();
        }
    }
}
