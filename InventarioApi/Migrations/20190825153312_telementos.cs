using Microsoft.EntityFrameworkCore.Migrations;

namespace InventarioApi.Migrations
{
    public partial class telementos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elementos_Telementos_TelementoId",
                table: "Elementos");

            migrationBuilder.AlterColumn<long>(
                name: "TelementoId",
                table: "Elementos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Elementos_Telementos_TelementoId",
                table: "Elementos",
                column: "TelementoId",
                principalTable: "Telementos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elementos_Telementos_TelementoId",
                table: "Elementos");

            migrationBuilder.AlterColumn<long>(
                name: "TelementoId",
                table: "Elementos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Elementos_Telementos_TelementoId",
                table: "Elementos",
                column: "TelementoId",
                principalTable: "Telementos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
