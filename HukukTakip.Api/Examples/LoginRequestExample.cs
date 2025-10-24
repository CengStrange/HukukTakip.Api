using HukukTakip.Api.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace HukukTakip.Api.Examples
{
    public class LoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples() => new()
        {
            Email = "admin@demo.com",
            Sifre = "Admin123!"
        };
    }
}
