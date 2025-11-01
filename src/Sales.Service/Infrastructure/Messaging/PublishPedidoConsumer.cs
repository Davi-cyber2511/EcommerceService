using MassTransit;
using Sales.Service.Domain.Entities;
using Shared.Contracts.Events;

namespace Sales.Service.Infrastructure.Messaging;
public class PublishPedidoConsumer : IConsumer<PedidoCriadoEvent>
{
    private readonly SalesDbContext _db;
    private readonly IPublishEndpoint _publish;
    public PublishPedidoConsumer(SalesDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    public async Task Consume(ConsumeContext<PedidoCriadoEvent> context)
    {
        var evt = context.Message;

        var pedido = new Pedido();
        pedido.Id = Guid.NewGuid();
        pedido.UsuarioId = evt.UsuarioId;
        pedido.Data = DateTimeOffset.UtcNow;
        pedido.Status = "AguardandoReserva";
        pedido.CreatedAt = DateTimeOffset.UtcNow;

        foreach (var iPedido in evt.Items)
        {
            var itemPedido = new ItemPedido();
            itemPedido.Id = Guid.NewGuid();
            itemPedido.ProdutoId = iPedido.ProdutoId;
            itemPedido.Quantidade = iPedido.Quantidade;
            itemPedido.PrecoUnitario = iPedido.PrecoUnitario;
            itemPedido.CreatedAt = DateTimeOffset.UtcNow;

            await _db.ItemPedido.AddAsync(itemPedido);
        }

        await _db.Pedido.AddAsync(pedido);
        await _db.SaveChangesAsync();
    }
}