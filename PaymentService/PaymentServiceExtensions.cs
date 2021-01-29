using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentManager.Contracts;
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
            services.AddTransient<IPaymentService, PaymentService>();
        }
    }
}
