using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Time",
                value: new DateTime(2019, 4, 9, 9, 52, 21, 163, DateTimeKind.Local).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Time",
                value: new DateTime(2019, 4, 9, 9, 52, 21, 163, DateTimeKind.Local).AddTicks(8978));

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Time",
                value: new DateTime(2019, 4, 9, 9, 52, 21, 163, DateTimeKind.Local).AddTicks(8994));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Joined", "LastActive" },
                values: new object[] { new DateTime(2019, 4, 9, 9, 52, 21, 161, DateTimeKind.Local).AddTicks(4752), new DateTime(2019, 4, 9, 9, 52, 21, 162, DateTimeKind.Local).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Joined", "LastActive" },
                values: new object[] { new DateTime(2019, 4, 9, 9, 52, 21, 162, DateTimeKind.Local).AddTicks(8281), new DateTime(2019, 4, 9, 9, 52, 21, 162, DateTimeKind.Local).AddTicks(8289) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Joined", "LastActive" },
                values: new object[] { new DateTime(2019, 4, 9, 9, 52, 21, 162, DateTimeKind.Local).AddTicks(8294), new DateTime(2019, 4, 9, 9, 52, 21, 162, DateTimeKind.Local).AddTicks(8296) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Time",
                value: new DateTime(2019, 4, 9, 9, 49, 16, 75, DateTimeKind.Local).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Time",
                value: new DateTime(2019, 4, 9, 9, 49, 16, 75, DateTimeKind.Local).AddTicks(2743));

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Time",
                value: new DateTime(2019, 4, 9, 9, 49, 16, 75, DateTimeKind.Local).AddTicks(2759));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Joined", "LastActive" },
                values: new object[] { new DateTime(2019, 4, 9, 9, 49, 16, 72, DateTimeKind.Local).AddTicks(7806), new DateTime(2019, 4, 9, 9, 49, 16, 74, DateTimeKind.Local).AddTicks(1616) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Joined", "LastActive" },
                values: new object[] { new DateTime(2019, 4, 9, 9, 49, 16, 74, DateTimeKind.Local).AddTicks(1885), new DateTime(2019, 4, 9, 9, 49, 16, 74, DateTimeKind.Local).AddTicks(1893) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Joined", "LastActive" },
                values: new object[] { new DateTime(2019, 4, 9, 9, 49, 16, 74, DateTimeKind.Local).AddTicks(1898), new DateTime(2019, 4, 9, 9, 49, 16, 74, DateTimeKind.Local).AddTicks(1900) });
        }
    }
}
