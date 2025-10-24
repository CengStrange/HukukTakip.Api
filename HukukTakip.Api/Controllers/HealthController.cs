using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HukukTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        // Not: DbContext sınıfının adını projendekiyle aynı yap.
        private readonly AppDb _db;
        public HealthController(AppDb db) => _db = db;

        /// <summary>Basit ping (herkese açık)</summary>
        [HttpGet("ping")]
        [AllowAnonymous]
        public IActionResult Ping() => Ok(new { ok = true, ts = DateTime.UtcNow });

        /// <summary>DB bağlantısı testi (herkese açık)</summary>
        [HttpGet("db")]
        [AllowAnonymous]
        public async Task<IActionResult> Db()
        {
            try
            {
                var can = await _db.Database.CanConnectAsync();
                return Ok(new { db = can });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { db = false, error = ex.Message });
            }
        }

        /// <summary>Token gerekli (kimlik bilgisi döner)</summary>
        [HttpGet("secure")]
        [Authorize]
        public IActionResult Secure()
        {
            var name = User.Identity?.Name
                       ?? User.FindFirstValue(ClaimTypes.Name)
                       ?? "unknown";

            var role = User.FindFirstValue(ClaimTypes.Role) ?? "none";

            return Ok(new { ok = true, user = name, role });
        }

        /// <summary>Yalnızca admin rolü</summary>
        [HttpGet("admin-only")]
        [Authorize(Roles = "admin")]
        public IActionResult AdminOnly() => Ok(new { ok = true, message = "admin erişimi onaylandı" });
    }
}
