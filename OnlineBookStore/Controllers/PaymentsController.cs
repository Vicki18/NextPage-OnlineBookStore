using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly AdminPaymentService _adminPaymentService;
    private readonly AdminOrderService _adminOrderService;

    public PaymentsController(AdminPaymentService adminPaymentService, AdminOrderService adminOrderService)
    {
        _adminPaymentService = adminPaymentService;
        _adminOrderService = adminOrderService;
    }

    // =========================
    // ADMIN: Payments
    // =========================
    [HttpGet("api/payments")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<PaymentDto>>> GetPayments(
        [FromQuery] string? search = null,
        [FromQuery] string? status = null,
        [FromQuery] int take = 300)
    {
        var list = await _adminPaymentService.GetPaymentsAsync(search, status, take);

        return Ok(list.Select(p => new PaymentDto(
            p.Id,
            p.OrderId,
            p.PaymentMethod,
            p.PaymentStatus,
            p.Amount,
            p.DatePaid
        )).ToList());
    }

    [HttpGet("api/payments/{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<PaymentDto>> GetPaymentDetails(int id)
    {
        var p = await _adminPaymentService.GetPaymentDetailsAsync(id);
        if (p is null) return NotFound();

        return Ok(new PaymentDto(p.Id, p.OrderId, p.PaymentMethod, p.PaymentStatus, p.Amount, p.DatePaid));
    }

    [HttpPut("api/payments/{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentUpdateDto input)
    {
        try
        {
            var updated = new Payment
            {
                Id = id,
                PaymentMethod = input.PaymentMethod,
                PaymentStatus = input.PaymentStatus,
                Amount = input.Amount,
                DatePaid = input.DatePaid
            };

            await _adminPaymentService.UpdatePaymentAsync(updated);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // =========================
    // ADMIN: Upsert payment by order (custom REST endpoint)
    // /api/orders/{id}/payment
    // =========================
    [HttpPut("api/orders/{orderId:int}/payment")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpsertPaymentForOrder(int orderId, [FromBody] PaymentUpdateDto input)
    {
        try
        {
            await _adminOrderService.UpsertPaymentAsync(
                orderId,
                input.PaymentMethod,
                input.PaymentStatus,
                input.DatePaid
            );
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
