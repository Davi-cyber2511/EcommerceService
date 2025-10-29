using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MassTransit;
using Shared.Contracts.Events;
using Microsoft.EntityFrameworkCore;

public class PedidoCriadoConsumer : IConsumer<PedidoCriadoEvent>
{
    private readonly StockDbContext _db;
    private readonly IPublishEndpoint _publish;
    public PedidoCriadoConsumer(StockDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    public async Task Consume(ConsumeContext<PedidoCriadoEvent> context)
    {
        var evt = context.Message;
        var insuficientes = new List<string>();

        foreach (var item in evt.Items)
        {
            var produto = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == item.ProdutoId);
            if (produto == null || produto.Quantidade < item.Quantidade)
            {
                insuficientes.Add(item.ProdutoId.ToString());
            }
        }

        if (insuficientes.Any())
        {
            // Aqui você poderia publicar EstoqueInsuficienteEvent (não implementado agora)
            return;
        }

        foreach (var item in evt.Items)
        {
            var produto = await _db.Produtos.FirstAsync(p => p.Id == item.ProdutoId);
            produto.Quantidade -= item.Quantidade;
        }

        await _db.SaveChangesAsync();
        await _publish.Publish(new EstoqueReservadoEvent(evt.PedidoId));
    }
}
