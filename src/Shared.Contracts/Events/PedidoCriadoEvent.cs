using Shared.Contracts.DTOs;

namespace Shared.Contracts.Events;
public record PedidoCriadoEvent(Guid Id, Guid UsuarioId, IEnumerable<OrderItemDto> Items, decimal Total);