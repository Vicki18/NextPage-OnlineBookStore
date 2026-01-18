using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
[Route("api/orderitems")]
[Authorize(Roles = "Administrator")]
public class OrderItemsController : ControllerBase
{
    private readonly AdminOrderItemService _adminOrderItemService;

    public OrderItemsController(AdminOrderItemService adminOrderItemService)
    {
        _adminOrderItemService = adminOrderItemService;
    }

    /// <summary>
    /// Admin list order items (custom filters).
    /// Examples:
    /// GET /api/orderitems?search=harry
    /// GET /api/orderitems?orderId=12
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<OrderItemDto>>> GetOrderItems(
        [FromQuery] string? search = null,
        [FromQuery] int? orderId = null,
        [FromQuery] int take = 300)
    {
        var list = await _adminOrderItemService.GetOrderItemsAsync(search, orderId, take);

        return Ok(list.Select(oi => new OrderItemDto(
            oi.Id,
            oi.OrderId,
            oi.BookId,
            oi.Book?.Title,
            oi.Quantity,
            oi.UnitPrice,
            oi.LineTotal
        )).ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
    {
        var oi = await _adminOrderItemService.GetOrderItemAsync(id);
        if (oi is null) return NotFound();

        return Ok(new OrderItemDto(
            oi.Id,
            oi.OrderId,
            oi.BookId,
            oi.Book?.Title,
            oi.Quantity,
            oi.UnitPrice,
            oi.LineTotal
        ));
    }
}
