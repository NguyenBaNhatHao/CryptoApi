using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "EnergyTN",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currencycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyTN", x => x.id);
                });

            
            migrationBuilder.CreateTable(
                name: "TokenTN",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currencycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recipientAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transactionRequestId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenTN", x => x.id);
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "EnergyTN");



            migrationBuilder.DropTable(
                name: "TokenTN");


        }
    }
}
