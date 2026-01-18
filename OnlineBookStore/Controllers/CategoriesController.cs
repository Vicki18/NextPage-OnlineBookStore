using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;
    private readonly AdminCategoryService _adminCategoryService;

    public CategoriesController(
        IDbContextFactory<OnlineBookStoreContext> dbFactory,
        AdminCategoryService adminCategoryService)
    {
        _dbFactory = dbFactory;
        _adminCategoryService = adminCategoryService;
    }

    // Public list
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories([FromQuery] string? search = null)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Category
            .AsNoTracking()
            .Include(c => c.Books)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            q = q.Where(c => (c.CategoryName ?? "").Contains(s) || (c.Description ?? "").Contains(s));
        }

        var categories = await q
            .OrderBy(c => c.CategoryName)
            .ThenByDescending(c => c.Id)
            .Select(c => new CategoryDto(c.Id, c.CategoryName, c.Description, c.Books != null ? c.Books.Count : 0))
            .ToListAsync();

        return Ok(categories);
    }

    // Public details (custom: includes category books)
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> GetCategory(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var c = await db.Category
            .AsNoTracking()
            .Include(x => x.Books!)
                .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (c is null) return NotFound();

        return Ok(new
        {
            category = new CategoryDto(c.Id, c.CategoryName, c.Description, c.Books?.Count ?? 0),
            books = (c.Books ?? new List<Book>()).Select(b => new BookDto(
                b.Id,
                b.Title,
                b.ISBN,
                b.Description,
                b.Price,
                b.StockQty,
                b.CoverImageUrl,
                b.AuthorId,
                b.Author?.Name,
                b.CategoryId,
                c.CategoryName
            ))
        });
    }

    // Admin CRUD
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<object>> CreateCategory([FromBody] Category input)
    {
        var id = await _adminCategoryService.CreateAsync(input, User.Identity?.Name);
        return CreatedAtAction(nameof(GetCategory), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category input)
    {
        try
        {
            input.Id = id;
            await _adminCategoryService.UpdateAsync(input, User.Identity?.Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _adminCategoryService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
