using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eimt.Persistence.Migrations
{
    public partial class addsector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SectorId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MenagerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Users_MenagerId",
                        column: x => x.MenagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SectorId",
                table: "Users",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_MenagerId",
                table: "Sectors",
                column: "MenagerId",
                unique: true,
                filter: "[MenagerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sectors_SectorId",
                table: "Users",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sectors_SectorId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropIndex(
                name: "IX_Users_SectorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Users");
        }
    }
}
