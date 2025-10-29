using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Pedido
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "AguardandoReserva";
    public List<ItemPedido> Itens { get; set; } = new();
    public decimal Total => Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
}
