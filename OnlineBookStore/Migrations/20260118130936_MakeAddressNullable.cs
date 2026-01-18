using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookStore.Migrations
{
    /// <inheritdoc />
    public partial class MakeAddressNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ Address already exists -> ALTER to nullable
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // ✅ DisplayName already exists -> ALTER to nullable
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // --- keep your seed date updates (unchanged) ---
            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7152), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7153) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7159), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7160) });

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7163), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7164) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7636), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7637) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7642), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7643) });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7648), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7649) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7491), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7492) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7496), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7496) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7500), new DateTime(2026, 1, 18, 21, 9, 35, 786, DateTimeKind.Local).AddTicks(7500) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert to NOT NULL (use defaultValue to avoid SQL error)
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // --- keep your seed date updates (unchanged) ---
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
        }
    }
}
