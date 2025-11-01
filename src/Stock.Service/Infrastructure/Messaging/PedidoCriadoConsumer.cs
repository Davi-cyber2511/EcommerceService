using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Events;
using Stock.Service.Domain.Entities;

public class PedidoCriadoConsumer : IConsumer<EstoqueReservadoEvent>
{
    private readonly StockDbContext _db;
    private readonly IPublishEndpoint _publish;
    public PedidoCriadoConsumer(StockDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    public async Task Consume(ConsumeContext<EstoqueReservadoEvent> context)
    {
        var evt = context.Message;
        var produtoExiste = await _db.Produto.FirstOrDefaultAsync(x => x.Nome.Trim().ToUpper() == evt.Nome.Trim().ToUpper());
        if (produtoExiste == null)
        {
            var produto = new Produto();
            produto.Id = Guid.NewGuid();
            produto.Nome = evt.Nome;
            produto.Descricao = evt.Descricao;
            produto.Preco = evt.Preco;
            produto.Descricao = evt.Descricao;
            produto.CreatedAt = DateTimeOffset.UtcNow;

            await _db.Produto.AddAsync(produto);
            await _db.SaveChangesAsync();
        }
        await Task.Delay(TimeSpan.FromSeconds(2));
    }
}
