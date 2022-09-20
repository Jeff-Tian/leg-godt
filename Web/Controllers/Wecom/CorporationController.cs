using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{name}")]
    public IActionResult GetCorp(string name)
    {
        throw new NotImplementedException();
    }
}