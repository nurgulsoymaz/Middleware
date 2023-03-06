using System.Net;
using MiddlewareExample.Web.Middlewares;

namespace MiddlewareExample.Web.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {

        private readonly RequestDelegate _requestDelegate;

        private const string WhiteIpAddress = "::1";

        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //IP adresi versiyonları
            //IPV4 version => 127.0.01 =>localhost 
            //IPV6 => gelecekteki versiyon =>::1 =>localhost

            var reqIpAddress = context.Connection.RemoteIpAddress;//data geliyor

            bool AnyWhiteIpAddress = IPAddress.Parse(WhiteIpAddress).Equals(reqIpAddress);

            if(AnyWhiteIpAddress==true)
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("Forbidden");
                //web sitesinde nereye gitsek forbidden gelecek
            }
        }
    }
}
