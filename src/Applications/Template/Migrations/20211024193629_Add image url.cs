using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class Addimageurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPU_Id",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "GPU_Id",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "HDD_Id",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "RAM_Id",
                table: "Laptops");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "RAM",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "HDD",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "GPU",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "CPU",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "RAM");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "HDD");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "GPU");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "CPU");

            migrationBuilder.AddColumn<string>(
                name: "CPU_Id",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GPU_Id",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HDD_Id",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RAM_Id",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
