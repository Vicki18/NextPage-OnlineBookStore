using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
public class CustomersController : ControllerBase
{
    private readonly AdminCustomerService _adminCustomerService;
    private readonly CustomerService _customerService;

    public CustomersController(AdminCustomerService adminCustomerService, CustomerService customerService)
    {
        _adminCustomerService = adminCustomerService;
        _customerService = customerService;
    }

    // =========================
    // ADMIN: Customers
    // =========================
    [HttpGet("api/customers")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<CustomerDto>>> GetCustomers([FromQuery] string? search = null, [FromQuery] int take = 200)
    {
        var list = await _adminCustomerService.GetCustomersAsync(search, take);

        return Ok(list.Select(c => new CustomerDto(
            c.Id,
            c.UserId,
            c.FullName,
            c.Email,
            c.Phone,
            c.Address,
            c.Orders?.Count ?? 0
        )).ToList());
    }

    [HttpGet("api/customers/{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<object>> GetCustomerDetails(int id)
    {
        var c = await _adminCustomerService.GetCustomerDetailsAsync(id);
        if (c is null) return NotFound();

        // Return customer + nested orders summary (custom API)
        var dto = new CustomerDto(c.Id, c.UserId, c.FullName, c.Email, c.Phone, c.Address, c.Orders?.Count ?? 0);

        var orders = (c.Orders ?? new List<Orders>())
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderSummaryDto(
                o.Id,
                o.OrderDate,
                o.Status,
                o.TotalAmount,
                o.CustomerId,
                c.FullName,
                c.Email,
                o.Payment?.PaymentMethod,
                o.Payment?.PaymentStatus
            ))
            .ToList();

        return Ok(new { customer = dto, orders });
    }

    // =========================
    // CUSTOMER: "Me" profile
    // =========================
    [HttpGet("api/me/customer")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<CustomerDto>> GetMyCustomerProfile()
    {
        try
        {
            var c = await _customerService.GetOrCreateCustomerForCurrentUserAsync(User);

            return Ok(new CustomerDto(
                c.Id,
                c.UserId,
                c.FullName,
                c.Email,
                c.Phone,
                c.Address,
                c.Orders?.Count ?? 0
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("api/me/customer")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<CustomerDto>> UpdateMyCustomerProfile([FromBody] CustomerUpdateDto input)
    {
        try
        {
            var c = await _customerService.GetOrCreateCustomerForCurrentUserAsync(User);

            var updated = await _customerService.UpdateCustomerProfileAsync(
                c.Id,
                input.FullName ?? c.FullName ?? "",
                input.Phone ?? c.Phone ?? "",
                input.Address ?? c.Address ?? ""
            );

            return Ok(new CustomerDto(
                updated.Id,
                updated.UserId,
                updated.FullName,
                updated.Email,
                updated.Phone,
                updated.Address,
                updated.Orders?.Count ?? 0
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
