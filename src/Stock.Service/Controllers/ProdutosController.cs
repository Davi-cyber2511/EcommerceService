using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly StockDbContext _db;
    public ProdutosController(StockDbContext db) { _db = db; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Produto produto)
    {
        produto.Id = Guid.NewGuid();
        produto.CreatedAt = DateTime.UtcNow;
        await _db.Produtos.AddAsync(produto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _db.Produtos.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var p = await _db.Produtos.FindAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}
