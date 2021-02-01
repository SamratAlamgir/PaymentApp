using BankApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentManager.Contracts;
using PaymentManager.Validators;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManager
{
    public static class PaymentServiceExtensions
    {
        public static void RegisterPaymentServiceTypes(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepositoryTypes(configuration);
            services.RegisterBankAppTypes();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddSingleton<ICardValidatorProvider, CardValidatorProvider>();
        }
    }
}
