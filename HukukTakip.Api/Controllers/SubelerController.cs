using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HukukTakip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubelerController : ControllerBase
{
    private readonly AppDb _db;
    public SubelerController(AppDb db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        
        var subeler = await _db.Subeler
            .AsNoTracking()
            .OrderBy(s => s.BrmAd)
            .Select(s => new { s.BrmKod, s.BrmAd }) 
            .ToListAsync(ct);

        return Ok(subeler);
    }
}
