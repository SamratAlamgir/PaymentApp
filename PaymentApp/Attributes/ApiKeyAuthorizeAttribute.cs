using Microsoft.AspNetCore.Mvc;
using PaymentApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Attributes
{

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class ApiKeyAuthorizeAttribute : TypeFilterAttribute
	{
		public ApiKeyAuthorizeAttribute() : base(typeof(ApiKeyAuthorizeAsyncFilter))
		{
		}
	}
}
