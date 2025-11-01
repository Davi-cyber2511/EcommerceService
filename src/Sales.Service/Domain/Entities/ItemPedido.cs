namespace Sales.Service.Domain.Entities;
public class ItemPedido
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}