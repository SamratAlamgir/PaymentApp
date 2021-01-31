using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Auth
{
    public interface IApiKeyService
    {
        Task<bool> IsAuthorized(string clientId, string apiKey);
    }
}
