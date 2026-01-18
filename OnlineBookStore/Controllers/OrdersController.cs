using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
public class OrdersController : ControllerBase
{
    private readonly AdminOrderService _adminOrderService;
    private readonly CustomerService _customerService;
    private readonly OrderService _orderService;

    public OrdersController(AdminOrderService adminOrderService, CustomerService customerService, OrderService orderService)
    {
        _adminOrderService = adminOrderService;
        _customerService = customerService;
        _orderService = orderService;
    }

    // =========================
    // ADMIN: Orders (list + filters)
    // =========================
    [HttpGet("api/orders")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<OrderSummaryDto>>> GetOrders(
        [FromQuery] string? search = null,
        [FromQuery] string? status = null,
        [FromQuery] int take = 200)
    {
        var list = await _adminOrderService.GetOrdersAsync(search, status, take);

        return Ok(list.Select(o => new OrderSummaryDto(
            o.Id,
            o.OrderDate,
            o.Status,
            o.TotalAmount,
            o.CustomerId,
            o.Customer?.FullName,
            o.Customer?.Email,
            o.Payment?.PaymentMethod,
            o.Payment?.PaymentStatus
        )).ToList());
    }

    [HttpGet("api/orders/{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<OrderDetailsDto>> GetOrderDetails(int id)
    {
        var o = await _adminOrderService.GetOrderDetailsAsync(id);
        if (o is null) return NotFound();

        var customerDto = o.Customer == null
            ? null
            : new CustomerDto(
                o.Customer.Id,
                o.Customer.UserId,
                o.Customer.FullName,
                o.Customer.Email,
                o.Customer.Phone,
                o.Customer.Address,
                o.Customer.Orders?.Count ?? 0
            );

        var paymentDto = o.Payment == null
            ? null
            : new PaymentDto(o.Payment.Id, o.Payment.OrderId, o.Payment.PaymentMethod, o.Payment.PaymentStatus, o.Payment.Amount, o.Payment.DatePaid);

        var items = (o.OrderItems ?? new List<OrderItem>()).Select(oi => new OrderItemDto(
            oi.Id,
            oi.OrderId,
            oi.BookId,
            oi.Book?.Title,
            oi.Quantity,
            oi.UnitPrice,
            oi.LineTotal
        )).ToList();

        return Ok(new OrderDetailsDto(o.Id, o.OrderDate, o.Status, o.TotalAmount, customerDto, paymentDto, items));
    }

    [HttpGet("api/orders/statuses")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<string>>> GetDistinctOrderStatuses()
        => Ok(await _adminOrderService.GetDistinctStatusesAsync());

    [HttpPut("api/orders/{id:int}/status")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto input)
    {
        try
        {
            await _adminOrderService.UpdateOrderStatusAsync(id, input.Status ?? "");
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // =========================
    // CUSTOMER: My Orders
    // =========================
    [HttpGet("api/me/orders")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<List<OrderSummaryDto>>> GetMyOrders()
    {
        try
        {
            var customer = await _customerService.GetOrCreateCustomerForCurrentUserAsync(User);
            var orders = await _orderService.GetOrdersForCustomerAsync(customer.Id);

            return Ok(orders.Select(o => new OrderSummaryDto(
                o.Id,
                o.OrderDate,
                o.Status,
                o.TotalAmount,
                o.CustomerId,
                customer.FullName,
                customer.Email,
                o.Payment?.PaymentMethod,
                o.Payment?.PaymentStatus
            )).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
