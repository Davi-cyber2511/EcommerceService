using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sales.Service.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<SalesDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PublishPedidoConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"]);
        cfg.ReceiveEndpoint("estoque-pedido-criado-queue", e => {
            e.ConfigureConsumer<PublishPedidoConsumer>(context);
        });
    });
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.MapControllers();
app.Run();