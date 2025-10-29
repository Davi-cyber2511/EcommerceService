using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.Events;
using MassTransit;

using MassTransit;
using Shared.Contracts.Events;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly SalesDbContext _db;
    private readonly IPublishEndpoint _publish;
    public PedidosController(SalesDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Pedido pedido)
    {
        pedido.Id = Guid.NewGuid();
        pedido.Data = DateTime.UtcNow;
        pedido.Status = "AguardandoReserva";

        foreach (var item in pedido.Itens)
            item.Id = Guid.NewGuid();

        await _db.Pedidos.AddAsync(pedido);
        await _db.SaveChangesAsync();

        var itemsDto = pedido.Itens.Select(i => new Shared.Contracts.DTOs.OrderItemDto(i.ProdutoId, i.Quantidade, i.PrecoUnitario));
        await _publish.Publish(new PedidoCriadoEvent(pedido.Id, pedido.UsuarioId, itemsDto, pedido.Total));

        return Accepted(new { pedidoId = pedido.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        var p = await _db.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}
