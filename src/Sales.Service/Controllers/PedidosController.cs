using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Service.Domain.Entities;
using Shared.Contracts.DTOs;
using Shared.Contracts.Events;

[ApiController]
[Route("[controller]")]
public class PedidosController : ControllerBase
{
    private readonly SalesDbContext _db;
    private readonly IPublishEndpoint _publish;
    public PedidosController(SalesDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    [HttpPost("estoque-pedido-criado-queue")]
    public async Task<IActionResult> Create([FromBody] Pedido pedido)
    {
        var itemsDto = pedido.Itens.Select(i => new OrderItemDto(i.ProdutoId, i.Quantidade, i.PrecoUnitario));
        await _publish.Publish(new PedidoCriadoEvent(pedido.Id, pedido.UsuarioId, itemsDto, pedido.Total));

        return Accepted(new { pedidoId = pedido.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        var p = await _db.Pedido.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}
