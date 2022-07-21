using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class AddnewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestPropery",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RAM",
                table: "Laptops",
                newName: "RAM_Id");

            migrationBuilder.RenameColumn(
                name: "HDD",
                table: "Laptops",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GPU",
                table: "Laptops",
                newName: "HDD_Id");

            migrationBuilder.RenameColumn(
                name: "CPU",
                table: "Laptops",
                newName: "GPU_Id");

            migrationBuilder.AddColumn<string>(
                name: "CPUId",
                table: "Laptops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPU_Id",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GPUId",
                table: "Laptops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HDDId",
                table: "Laptops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Laptops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RAMId",
                table: "Laptops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Laptops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CPU",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountCores = table.Column<int>(type: "int", nullable: false),
                    Hz = table.Column<double>(type: "float", nullable: false),
                    Cache = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPU", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPU_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GPU",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemoryCapacity = table.Column<int>(type: "int", nullable: false),
                    MemoryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPU", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPU_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HDD",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cache = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HDD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HDD_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RAM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemoryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemoryCapacity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAM_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_CPUId",
                table: "Laptops",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_GPUId",
                table: "Laptops",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_HDDId",
                table: "Laptops",
                column: "HDDId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_RAMId",
                table: "Laptops",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_UserId",
                table: "Laptops",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CPU_UserId",
                table: "CPU",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GPU_UserId",
                table: "GPU",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HDD_UserId",
                table: "HDD",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RAM_UserId",
                table: "RAM",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_CPU_CPUId",
                table: "Laptops",
                column: "CPUId",
                principalTable: "CPU",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_GPU_GPUId",
                table: "Laptops",
                column: "GPUId",
                principalTable: "GPU",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_HDD_HDDId",
                table: "Laptops",
                column: "HDDId",
                principalTable: "HDD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_RAM_RAMId",
                table: "Laptops",
                column: "RAMId",
                principalTable: "RAM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Users_UserId",
                table: "Laptops",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_CPU_CPUId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_GPU_GPUId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_HDD_HDDId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_RAM_RAMId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Users_UserId",
                table: "Laptops");

            migrationBuilder.DropTable(
                name: "CPU");

            migrationBuilder.DropTable(
                name: "GPU");

            migrationBuilder.DropTable(
                name: "HDD");

            migrationBuilder.DropTable(
                name: "RAM");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_CPUId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_GPUId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_HDDId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_RAMId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_UserId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CPUId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CPU_Id",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "GPUId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "HDDId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "RAMId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Laptops");

            migrationBuilder.RenameColumn(
                name: "RAM_Id",
                table: "Laptops",
                newName: "RAM");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Laptops",
                newName: "HDD");

            migrationBuilder.RenameColumn(
                name: "HDD_Id",
                table: "Laptops",
                newName: "GPU");

            migrationBuilder.RenameColumn(
                name: "GPU_Id",
                table: "Laptops",
                newName: "CPU");

            migrationBuilder.AddColumn<string>(
                name: "TestPropery",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
