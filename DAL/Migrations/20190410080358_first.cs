using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserName = table.Column<string>(maxLength: 64, nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(maxLength: 64, nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    MovieId = table.Column<int>(nullable: false),
                    Value = table.Column<short>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Image", "Time", "Title", "Url", "UserName" },
                values: new object[] { 1, "https://cdn.myanimelist.net/images/anime/12/76049.jpg", new DateTime(2019, 4, 10, 11, 3, 57, 656, DateTimeKind.Local).AddTicks(1176), "One Punch Man", "https://myanimelist.net/anime/30276/One_Punch_Man", "catnib" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Image", "Time", "Title", "Url", "UserName" },
                values: new object[] { 2, "https://cdn.myanimelist.net/images/anime/3/77176.jpg", new DateTime(2019, 4, 10, 11, 3, 57, 657, DateTimeKind.Local).AddTicks(5112), "Mobile Suit Gundam Thunderbolt", "https://myanimelist.net/anime/31973/Mobile_Suit_Gundam_Thunderbolt", "siegrest" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Image", "Time", "Title", "Url", "UserName" },
                values: new object[] { 3, "https://cdn.myanimelist.net/images/anime/1562/100460.jpg", new DateTime(2019, 4, 10, 11, 3, 57, 657, DateTimeKind.Local).AddTicks(5128), "Fairy Gone", "https://myanimelist.net/anime/39063/Fairy_Gone", "rinnex" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Content", "MovieId" },
                values: new object[,]
                {
                    { 1, "action", 1 },
                    { 2, "really cool", 1 },
                    { 3, "military", 2 },
                    { 4, "michael bay", 2 },
                    { 5, "average at best", 2 },
                    { 6, "eh", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MovieId",
                table: "Tags",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MovieId",
                table: "Votes",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
