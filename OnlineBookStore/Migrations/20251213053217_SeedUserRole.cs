using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(48));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(49));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(51));

            migrationBuilder.UpdateData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(225));

            migrationBuilder.UpdateData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(227));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "OrderDate" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(129), new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(126) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DatePaid" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(323), new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(322) });

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 32, 16, 312, DateTimeKind.Local).AddTicks(416));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8454), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8454) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8456), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8456) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8458), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8458) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8296), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8296) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8301), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8301) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8303), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8304) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8519), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8520) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8521), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8522) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8523), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8524) });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8591));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8592));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8594));

            migrationBuilder.UpdateData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8724));

            migrationBuilder.UpdateData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8727));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "OrderDate" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8662), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8659) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DatePaid" },
                values: new object[] { new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8789), new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8788) });

            migrationBuilder.UpdateData(
                table: "Review",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 12, 13, 13, 4, 35, 790, DateTimeKind.Local).AddTicks(8851));
        }
    }
}
