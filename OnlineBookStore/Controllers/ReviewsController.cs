using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly AdminReviewService _adminReviewService;

    public ReviewsController(AdminReviewService adminReviewService)
    {
        _adminReviewService = adminReviewService;
    }

    /// <summary>
    /// Admin: list all reviews (supports search).
    /// Example: GET /api/reviews?search=5
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<ReviewDto>>> GetReviews([FromQuery] string? search = null)
    {
        var list = await _adminReviewService.GetReviewsAsync(search);
        return Ok(list.Select(r => new ReviewDto(
            r.Id,
            r.BookId,
            r.CustomerId,
            r.Customer?.FullName,
            r.Customer?.Email,
            r.Rating,
            r.Comment,
            r.DateCreated
        )).ToList());
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ReviewDto>> GetReview(int id)
    {
        var r = await _adminReviewService.GetReviewAsync(id);
        if (r is null) return NotFound();

        return Ok(new ReviewDto(
            r.Id,
            r.BookId,
            r.CustomerId,
            r.Customer?.FullName,
            r.Customer?.Email,
            r.Rating,
            r.Comment,
            r.DateCreated
        ));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] Review input)
    {
        try
        {
            input.Id = id;
            await _adminReviewService.UpdateAsync(input, User.Identity?.Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        try
        {
            await _adminReviewService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
