using Auth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.test.Middlewares
{
    public class AuthenticationMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public AuthenticationMiddleware(ILogger<AuthenticationMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var accessToken = context.Request.Headers["Authorization"].ToString().Split("bearer ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);
                var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
                var userName = tokenS.Claims.First(c=> c.Type.Equals("unique_name")).Value;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                    "https://localhost:44365/api/Authenticate/validate");
                request.Content = new StringContent("{\"name\":\""+userName+" \",\"token\":\""+ accessToken + "\"}" ,
                    Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                if(!responseString.Equals(StatusCodes.Status202Accepted.ToString()) ) {
                    _logger.LogError(responseString);
                    throw new Exception(responseString);
                }
                await next(context);
            }
            catch (Exception ex)
            {
                var message = CreateMessage(context, ex);
                _logger.LogError(message, ex);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            
            int statusCode;

            if (e is ArgumentException || e is ArgumentNullException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
            }

            _logger.LogError(e, e.Message);

            var response = JsonConvert.SerializeObject(statusCode, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private string CreateMessage(HttpContext context, Exception e)
        {
            var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

            if (e.InnerException != null)
            {
                message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";
            }

            return $"{message} RequestId: {context.TraceIdentifier}";
        }
    }    
}
