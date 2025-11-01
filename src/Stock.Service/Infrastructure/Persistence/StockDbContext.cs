using Microsoft.EntityFrameworkCore;
using Stock.Service.Domain.Entities;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) {}
    public DbSet<Produto> Produto { get; set; }
}
