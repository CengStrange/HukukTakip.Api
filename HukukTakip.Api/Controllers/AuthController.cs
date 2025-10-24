using HukukTakip.Api.Application.DTOs;
using HukukTakip.Api.Infrastructure.Data;
using HukukTakip.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace HukukTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDb _db;
        private readonly TokenService _token;

        public  AuthController(AppDb db, TokenService token)
        {
            _db = db;
            _token = token;
        }

        /// <summary>Giriş ve JWT üretimi</summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Kullanıcı girişi", Description = "Email + şifre ile JWT döner.")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user is null) return Unauthorized("Email veya şifre hatalı");

            var ok = BCrypt.Net.BCrypt.Verify(dto.Sifre, user.SifreHash);
            if (!ok) return Unauthorized("Email veya şifre hatalı");

            var (token, expires) = _token.CreateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                ExpiresAt = expires,
                Rol = user.Rol,
                KullaniciAdi = user.KullaniciAdi,
                Email = user.Email
            });
        }

        /// <summary>Aktif kullanıcı bilgisi (token’dan)</summary>
        [HttpGet("me")]
        [Authorize]
        [SwaggerOperation(Summary = "Aktif kullanıcı", Description = "JWT içindeki claim’leri döner.")]
        public IActionResult Me()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
                       ?? User.FindFirstValue(ClaimTypes.Actor)
                       ?? User.FindFirstValue(ClaimTypes.Sid);

            return Ok(new { id = sub, name, email, role });
        }

        /// <summary>Yalnızca admin erişebilir</summary>
        [HttpGet("admin-only")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Admin testi", Description = "Sadece admin rolüne izin verir.")]
        public IActionResult AdminOnly() => Ok("Admin alanı ✅");
    }
}
