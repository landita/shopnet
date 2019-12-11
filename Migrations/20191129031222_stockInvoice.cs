using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace shotnet.Migrations
{
    public partial class stockInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(nullable: true),
                    cardName = table.Column<string>(nullable: true),
                    cardNumber = table.Column<string>(nullable: true),
                    expiration = table.Column<DateTime>(nullable: false),
                    cvv = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invoice_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    totalPrice = table.Column<double>(nullable: false),
                    invoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock", x => x.id);
                    table.ForeignKey(
                        name: "FK_stock_Invoice_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "Invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1813a114-1185-490f-9983-949f3ab62d2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bfeaf9bf-5ae4-4cdc-8989-e6dbc5a53e10");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_userId",
                table: "Invoice",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_stock_invoiceId",
                table: "stock",
                column: "invoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1ea81c2d-b90c-45dd-8959-7a57881e69ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "49d9b03c-a1d6-4627-b9de-18cdac9cc208");
        }
    }
}
