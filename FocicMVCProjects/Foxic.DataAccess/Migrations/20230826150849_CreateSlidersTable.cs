using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic.DataAccess.Migrations
{
    public partial class CreateSlidersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderAbout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
