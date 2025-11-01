using Microsoft.EntityFrameworkCore;
using Sales.Service.Domain.Entities;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) {}
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<ItemPedido> ItemPedido { get; set; }
}
