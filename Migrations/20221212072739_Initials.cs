using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoApi.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Network",
                columns: table => new
                {
                    Networkid = table.Column<int>(type: "int", nullable: false),
                    NetworkName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => x.Networkid);
                });

            
            migrationBuilder.CreateTable(
                name: "SymbolCoin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Blockchain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Networkid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolCoin", x => x.id);
                    table.ForeignKey(
                        name: "FK_SymbolCoin_Network_Networkid",
                        column: x => x.Networkid,
                        principalTable: "Network",
                        principalColumn: "Networkid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymbolCoin_Networkid",
                table: "SymbolCoin",
                column: "Networkid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "SymbolCoin");


            migrationBuilder.DropTable(
                name: "Network");
        }
    }
}
