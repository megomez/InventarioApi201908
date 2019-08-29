using Microsoft.EntityFrameworkCore.Migrations;

namespace InventarioApi.Migrations
{
    public partial class tdocumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Tdocuemntos_TdocumentoId",
                table: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tdocuemntos",
                table: "Tdocuemntos");

            migrationBuilder.RenameTable(
                name: "Tdocuemntos",
                newName: "Tdocumentos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tdocumentos",
                table: "Tdocumentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Tdocumentos_TdocumentoId",
                table: "Personas",
                column: "TdocumentoId",
                principalTable: "Tdocumentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Tdocumentos_TdocumentoId",
                table: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tdocumentos",
                table: "Tdocumentos");

            migrationBuilder.RenameTable(
                name: "Tdocumentos",
                newName: "Tdocuemntos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tdocuemntos",
                table: "Tdocuemntos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Tdocuemntos_TdocumentoId",
                table: "Personas",
                column: "TdocumentoId",
                principalTable: "Tdocuemntos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
