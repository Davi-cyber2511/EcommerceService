using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Events;
using Stock.Service.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly StockDbContext _db;
    private readonly IPublishEndpoint _publish;
    public ProdutosController(StockDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    [HttpPost("estoque-pedido-criado-queue")]
    public async Task<IActionResult> Create([FromBody] Produto produto)
    {
        await _publish.Publish(new EstoqueReservadoEvent(produto.Nome, produto.Descricao, produto.Preco, produto.Quantidade));
        return CreatedAtAction(nameof(Create), new { id = produto.Id }, produto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.Produto.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var p = await _db.Produto.FindAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}
