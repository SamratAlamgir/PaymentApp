using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Auth
{
    public class ApiKeyService : IApiKeyService
    {
        public ApiKeyService()
        {

        }

        public async Task<bool> IsAuthorized(string clientId, string apiKey)
        {
            await Task.Delay(1);
            var isValid = clientId == "BD3B252E-904B-4830-BE54-D8C30A964577" && apiKey == "testapikey" ;
            return isValid;
        }
    }
}
