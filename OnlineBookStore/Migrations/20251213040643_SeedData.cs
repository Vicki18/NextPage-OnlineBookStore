using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineBookStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Bio", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7380), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7380), "M. Shwe", "System" },
                    { 2, null, "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7382), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7382), "A. Tan", "System" },
                    { 3, null, "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7384), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7384), "K. Lim", "System" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "DateCreated", "DateUpdated", "Description", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Fiction", "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7443), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7444), "Novels and stories (literary, romance, mystery, fantasy).", "System" },
                    { 2, "Non-Fiction", "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7446), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7446), "Real-world topics (biography, business, self-help, history).", "System" },
                    { 3, "Education", "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7448), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7448), "Learning materials (programming, textbooks, exam guides).", "System" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "CreatedBy", "DateCreated", "DateUpdated", "Email", "FullName", "Phone", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Tampines, Singapore", null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7541), null, "john.tan@email.com", "John Tan", "91234567", null },
                    { 2, "Jurong East, Singapore", null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7542), null, "sarah.lim@email.com", "Sarah Lim", "98765432", null },
                    { 3, "Woodlands, Singapore", null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7544), null, "daniel.wong@email.com", "Daniel Wong", "90112233", null }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedBy", "DateCreated", "DateUpdated", "Description", "ISBN", "Price", "StockQty", "Title", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, 1, null, "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7205), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7206), "A cozy fiction story set in a hidden bookstore.", "978-1111111111", 18.90m, 10, "The Silent Shelf", "System" },
                    { 2, 2, 2, null, "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7212), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7213), "Understanding patterns in real-world datasets.", "978-2222222222", 22.50m, 5, "Data in the City", "System" },
                    { 3, 3, 3, null, "System", new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7215), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7215), "A practical guide to Blazor development.", "978-3333333333", 28.00m, 7, "C# for Busy Students", "System" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedBy", "CustomerId", "DateCreated", "DateUpdated", "OrderDate", "Status", "TotalAmount", "UpdatedBy" },
                values: new object[] { 1, null, 1, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7622), null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7620), "Completed", 41.40m, null });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "BookId", "CreatedBy", "DateCreated", "DateUpdated", "LineTotal", "OrderId", "Quantity", "UnitPrice", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7700), null, 18.90m, 1, 1, 18.90m, null },
                    { 2, 2, null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7702), null, 22.50m, 1, 1, 22.50m, null }
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Amount", "CreatedBy", "DateCreated", "DatePaid", "DateUpdated", "OrderId", "PaymentMethod", "PaymentStatus", "UpdatedBy" },
                values: new object[] { 1, 41.40m, null, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7767), new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7766), null, 1, "Credit Card", "Paid", null });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "BookId", "Comment", "CreatedBy", "CustomerId", "DateCreated", "DateUpdated", "Rating", "UpdatedBy" },
                values: new object[] { 1, 1, "Excellent book!", null, 1, new DateTime(2025, 12, 13, 12, 6, 43, 222, DateTimeKind.Local).AddTicks(7844), null, 5, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
