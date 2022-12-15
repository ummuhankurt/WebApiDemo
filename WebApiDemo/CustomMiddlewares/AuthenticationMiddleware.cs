using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string autHeader = context.Request.Headers["Authorization"];

            //authenticationTürü boşluk( ) kullanıcıAdı:sifre
            //basic ummuhan:12345
            // StringComparison.OrdinalIgnoreCase -> harf duyarını devre dışı bırakır.
            if (autHeader != null && autHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase)) //Headeri var mı? ve basicle başlıyor mu?
            {
                var token = autHeader.Substring(6).Trim();
                var credentialString = "";
                try
                {
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch
                {

                    context.Response.StatusCode = 500;

                }


                var credantials = credentialString.Split(':');
                if (credantials[0] == "ummuhan" && credantials[1] == "12345")
                {
                    var claims = new[]
                    {
                        new Claim("name",credantials[0]),
                        new Claim(ClaimTypes.Role,"Admin")
                    };
                    var identity = new ClaimsIdentity(claims, "basic");
                    context.User = new ClaimsPrincipal(identity);
                }
            }

            else
            {
                context.Response.StatusCode = 401;
            }

            await _next(context);
        }
    }
}
