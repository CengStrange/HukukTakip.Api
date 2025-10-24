using HukukTakip.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HukukTakip.Api.Controllers;

[ApiController]
[Route("api/Sozluk/ilceler")] 
[Authorize]
public class IlcelerController : ControllerBase
{
    private readonly AppDb _db;
    public IlcelerController(AppDb db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? sehirId, CancellationToken ct)
    {
        var query = _db.Ilceler.AsNoTracking();

        if (sehirId.HasValue)
        {
            query = query.Where(i => i.SehirId == sehirId.Value);
        }

        var ilceler = await query
            .OrderBy(s => s.Ad)
            .Select(s => new { s.Id, s.Ad })
            .ToListAsync(ct);

        return Ok(ilceler);
    }
}



