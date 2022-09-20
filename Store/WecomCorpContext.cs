using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Store;

public class WecomCorpContext : DbContext
{
    public WecomCorpContext(DbContextOptions<WecomCorpContext> options) : base(options)
    {
        
    }
    
    public DbSet<Corporation> WecomCorps { get; set; }
}
