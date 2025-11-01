using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<StockDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PedidoCriadoConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"]);
        cfg.ReceiveEndpoint("estoque-pedido-criado-queue", e => {
            e.ConfigureConsumer<PedidoCriadoConsumer>(context);
        });
    });
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.MapControllers();
app.Run();
