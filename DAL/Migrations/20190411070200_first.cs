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
                    Score = table.Column<double>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    TrailerUrl = table.Column<string>(nullable: true),
                    Episodes = table.Column<int>(nullable: false),
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
                    Value = table.Column<short>(nullable: false),
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
