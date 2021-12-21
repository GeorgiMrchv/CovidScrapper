using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidScrapper.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CovidStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    TotalCases = table.Column<string>(nullable: true),
                    TotalTests = table.Column<string>(nullable: true),
                    ActiveCases = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidStatistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CovidStatistics");
        }
    }
}
