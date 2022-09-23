using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store;
using Web.Models;

namespace Web.Controllers.Wecom;

[Route("api/wecom/[controller]")]
[ApiController]
public class CorporationController : ControllerBase
{
    private readonly WecomCorpContext _context;

    public CorporationController(WecomCorpContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult<Corporation>> SaveCorp(Corporation corp)
    {
        if (_context.WecomCorps == null)
        {
            return Problem("Entity set 'WecomCorpContext.WecomCorps' is null.");
        }

        _context.WecomCorps.Add(corp);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCorp", new { name = corp.Name }, corp);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Corporation>> UpdateCorp(long id, Corporation corp)
    {
        if (id != corp.Id)
        {
            return BadRequest();
        }

        _context.Entry(corp).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Corporation>> GetCorp(string name)
    {
        return await _context.WecomCorps.FirstAsync(x => x.Name.Equals(name));
    }
}