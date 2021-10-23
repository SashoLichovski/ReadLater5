using Data.Interfaces;
using Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Web.Mvc;

namespace ReadLater5.Attributes
{
    public class ApiAuthorizationFilter : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        private readonly ClientAccess clientAccess;

        public ApiAuthorizationFilter(ClientAccess clientAccess)
        {
            this.clientAccess = clientAccess;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var clientRepository =
            context.HttpContext.RequestServices.GetService(typeof(IClientRepository))
                as IClientRepository;

            var authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader.Any() && authHeader[0].Contains("Basic"))
            {
                var hash = authHeader[0][6..];

                var encodedString = Base64Decode(hash);

                var splitData = encodedString.Split(":");

                var apiUser = splitData[0];
                var apiKey = splitData[1];

                var client = clientRepository.GetByApiUser(apiUser);
                if (client != null && client.Access == ClientAccess.FullAccess)
                    return;

                if (client == null)
                {
                    context.Result = new ObjectResult("Client not found") { StatusCode = 400 };
                }
                else if (client != null && client.ApiKey != apiKey)
                {
                    context.Result = new ObjectResult("Unauthorized") { StatusCode = 401 };
                }
                else if (client != null && client.Access != clientAccess)
                {
                    context.Result = new ObjectResult("Forbidden") { StatusCode = 403 };
                }
            }
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.ASCII.GetString(base64EncodedBytes);
        }
    }
}
