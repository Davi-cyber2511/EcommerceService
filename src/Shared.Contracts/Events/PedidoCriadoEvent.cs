using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Contracts.Events
{
    public record PedidoCriadoEvent(Guid PedidoId, Guid UsuarioId, IEnumerable<OrderItemDto> Items, decimal Total);
}