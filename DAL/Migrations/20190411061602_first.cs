using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    AddedBy = table.Column<string>(maxLength: 32, nullable: true),
                    MalId = table.Column<int>(nullable: true),
                    Score = table.Column<double>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    TrailerUrl = table.Column<string>(nullable: true),
                    Episodes = table.Column<int>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Synopsis = table.Column<string>(nullable: true),
                    Genres = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    SubmissionId = table.Column<int>(nullable: false),
                    Value = table.Column<short>(type: "BIT(1)", nullable: false),
                    Ip = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "AddedBy", "Duration", "Episodes", "Genres", "ImageUrl", "MalId", "Rating", "Score", "Synopsis", "Time", "Title", "TrailerUrl", "Type", "Url" },
                values: new object[] { 1, "catnib", null, null, null, "https://cdn.myanimelist.net/images/anime/12/76049.jpg", null, null, null, null, new DateTime(2019, 4, 11, 9, 16, 1, 741, DateTimeKind.Local).AddTicks(3632), "One Punch Man", null, null, "https://myanimelist.net/anime/30276/One_Punch_Man" });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "AddedBy", "Duration", "Episodes", "Genres", "ImageUrl", "MalId", "Rating", "Score", "Synopsis", "Time", "Title", "TrailerUrl", "Type", "Url" },
                values: new object[] { 2, "siegrest", null, null, null, "https://cdn.myanimelist.net/images/anime/3/77176.jpg", null, null, null, null, new DateTime(2019, 4, 11, 9, 16, 1, 742, DateTimeKind.Local).AddTicks(7496), "Mobile Suit Gundam Thunderbolt", null, null, "https://myanimelist.net/anime/31973/Mobile_Suit_Gundam_Thunderbolt" });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "AddedBy", "Duration", "Episodes", "Genres", "ImageUrl", "MalId", "Rating", "Score", "Synopsis", "Time", "Title", "TrailerUrl", "Type", "Url" },
                values: new object[] { 3, "rinnex", null, null, null, "https://cdn.myanimelist.net/images/anime/1562/100460.jpg", null, null, null, null, new DateTime(2019, 4, 11, 9, 16, 1, 742, DateTimeKind.Local).AddTicks(7512), "Fairy Gone", null, null, "https://myanimelist.net/anime/39063/Fairy_Gone" });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_MalId",
                table: "Submissions",
                column: "MalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_SubmissionId",
                table: "Votes",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Submissions");
        }
    }
}
