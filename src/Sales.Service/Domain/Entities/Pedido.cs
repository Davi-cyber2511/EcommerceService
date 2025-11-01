namespace Sales.Service.Domain.Entities;
public class Pedido
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTimeOffset Data { get; set; }
    public string Status { get; set; } = "AguardandoReserva";
    public List<ItemPedido> Itens { get; set; } = new();
    public decimal Total => Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
    public DateTimeOffset CreatedAt { get; set; }
}
