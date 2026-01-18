namespace OnlineBookStore.Controllers.Dto;

// --------------------
// CUSTOMER
// --------------------
public record CustomerDto(
    int Id,
    string? UserId,
    string? FullName,
    string? Email,
    string? Phone,
    string? Address,
    int OrdersCount
);

public record CustomerUpdateDto(
    string? FullName,
    string? Phone,
    string? Address
);

// --------------------
// ORDER / ORDER ITEMS
// --------------------
public record OrderSummaryDto(
    int Id,
    DateTime OrderDate,
    string? Status,
    decimal TotalAmount,
    int CustomerId,
    string? CustomerName,
    string? CustomerEmail,
    string? PaymentMethod,
    string? PaymentStatus
);

public record OrderDetailsDto(
    int Id,
    DateTime OrderDate,
    string? Status,
    decimal TotalAmount,
    CustomerDto? Customer,
    PaymentDto? Payment,
    List<OrderItemDto> Items
);

public record OrderItemDto(
    int Id,
    int OrderId,
    int BookId,
    string? BookTitle,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal
);

public record OrderStatusUpdateDto(
    string? Status
);

// --------------------
// PAYMENT
// --------------------
public record PaymentDto(
    int Id,
    int OrderId,
    string? PaymentMethod,
    string? PaymentStatus,
    decimal Amount,
    DateTime? DatePaid
);

public record PaymentUpdateDto(
    string? PaymentMethod,
    string? PaymentStatus,
    decimal Amount,
    DateTime? DatePaid
);
