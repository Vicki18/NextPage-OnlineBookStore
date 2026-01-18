using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Controllers.Dto;
using OnlineBookStore.Data;
using OnlineBookStore.Domain;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IDbContextFactory<OnlineBookStoreContext> _dbFactory;
    private readonly AdminAuthorService _adminAuthorService;

    public AuthorsController(
        IDbContextFactory<OnlineBookStoreContext> dbFactory,
        AdminAuthorService adminAuthorService)
    {
        _dbFactory = dbFactory;
        _adminAuthorService = adminAuthorService;
    }

    // Public list
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<AuthorDto>>> GetAuthors([FromQuery] string? search = null)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var q = db.Author
            .AsNoTracking()
            .Include(a => a.Books)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            q = q.Where(a => (a.Name ?? "").Contains(s) || (a.Bio ?? "").Contains(s));
        }

        var authors = await q
            .OrderBy(a => a.Name)
            .ThenByDescending(a => a.Id)
            .Select(a => new AuthorDto(a.Id, a.Name, a.Bio, a.Books != null ? a.Books.Count : 0))
            .ToListAsync();

        return Ok(authors);
    }

    // Public details (custom: includes author's books)
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> GetAuthor(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        var a = await db.Author
            .AsNoTracking()
            .Include(x => x.Books!)
                .ThenInclude(b => b.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (a is null) return NotFound();

        return Ok(new
        {
            author = new AuthorDto(a.Id, a.Name, a.Bio, a.Books?.Count ?? 0),
            books = (a.Books ?? new List<Book>()).Select(b => new BookDto(
                b.Id,
                b.Title,
                b.ISBN,
                b.Description,
                b.Price,
                b.StockQty,
                b.CoverImageUrl,
                b.AuthorId,
                a.Name,
                b.CategoryId,
                b.Category?.CategoryName
            ))
        });
    }

    // Admin CRUD
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<object>> CreateAuthor([FromBody] Author input)
    {
        var id = await _adminAuthorService.CreateAsync(input, User.Identity?.Name);
        return CreatedAtAction(nameof(GetAuthor), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author input)
    {
        try
        {
            input.Id = id;
            await _adminAuthorService.UpdateAsync(input, User.Identity?.Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            await _adminAuthorService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
