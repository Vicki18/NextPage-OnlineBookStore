using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineBookStore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTransactionalSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(8822), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(8823) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(8827), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(8827) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(8828), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(8829) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9118), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9118) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9121), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9121) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9123), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9124) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9035), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9036) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9038), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9038) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9040), new DateTime(2026, 1, 16, 15, 44, 43, 188, DateTimeKind.Local).AddTicks(9040) });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9867), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9868) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9870), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9871), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9872) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9606), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9607) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9614), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9614) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9616), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9617) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9973), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9974) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9975), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9976) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9977), new DateTime(2025, 12, 13, 13, 32, 16, 311, DateTimeKind.Local).AddTicks(9978) });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "CreatedBy", "DateCreated", "DateUpdated", "Email", "FullName", "Phone", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Tampines, Singapore", null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(48), null, "john.tan@email.com", "John Tan", "91234567", null },
                    { 2, "Jurong East, Singapore", null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(49), null, "sarah.lim@email.com", "Sarah Lim", "98765432", null },
                    { 3, "Woodlands, Singapore", null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(51), null, "daniel.wong@email.com", "Daniel Wong", "90112233", null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedBy", "CustomerId", "DateCreated", "DateUpdated", "OrderDate", "Status", "TotalAmount", "UpdatedBy" },
                values: new object[] { 1, null, 1, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(129), null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(126), "Completed", 41.40m, null });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "BookId", "Comment", "CreatedBy", "CustomerId", "DateCreated", "DateUpdated", "Rating", "UpdatedBy" },
                values: new object[] { 1, 1, "Excellent book!", null, 1, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(416), null, 5, null });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "BookId", "CreatedBy", "DateCreated", "DateUpdated", "LineTotal", "OrderId", "Quantity", "UnitPrice", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(225), null, 18.90m, 1, 1, 18.90m, null },
                    { 2, 2, null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(227), null, 22.50m, 1, 1, 22.50m, null }
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Amount", "CreatedBy", "DateCreated", "DatePaid", "DateUpdated", "OrderId", "PaymentMethod", "PaymentStatus", "UpdatedBy" },
                values: new object[] { 1, 41.40m, null, new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(323), new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(322), null, 1, "Credit Card", "Paid", null });
        }
    }
}
