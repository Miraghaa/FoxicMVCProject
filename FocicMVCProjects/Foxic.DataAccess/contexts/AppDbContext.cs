using Foxic.Core.Entities.AreasEntitycontroller;
using Microsoft.EntityFrameworkCore;

namespace Foxic.DataAccess.contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Slider> Sliders { get; set; } = null!;

}
