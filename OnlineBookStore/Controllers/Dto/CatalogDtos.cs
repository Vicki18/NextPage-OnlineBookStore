namespace OnlineBookStore.Controllers.Dto;

public record AuthorDto(
    int Id,
    string? Name,
    string? Bio,
    int BookCount
);

public record CategoryDto(
    int Id,
    string? CategoryName,
    string? Description,
    int BookCount
);

public record BookDto(
    int Id,
    string? Title,
    string? ISBN,
    string? Description,
    decimal Price,
    int StockQty,
    string? CoverImageUrl,
    int AuthorId,
    string? AuthorName,
    int CategoryId,
    string? CategoryName
);

public record BookUpsertDto(
    string? Title,
    string? ISBN,
    string? Description,
    decimal Price,
    int StockQty,
    string? CoverImageUrl,
    int AuthorId,
    int CategoryId
);

public record ReviewDto(
    int Id,
    int BookId,
    int CustomerId,
    string? CustomerName,
    string? CustomerEmail,
    int Rating,
    string? Comment,
    DateTime? DateCreated
);

public record ReviewCreateDto(
    int Rating,
    string? Comment
);
