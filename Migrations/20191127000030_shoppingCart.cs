using Microsoft.EntityFrameworkCore.Migrations;

namespace shotnet.Migrations
{
    public partial class shoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shoppingCart",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCart", x => new { x.userId, x.productId });
                    table.ForeignKey(
                        name: "FK_shoppingCart_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shoppingCart_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCart_productId",
                table: "shoppingCart",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoppingCart");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "35a004e2-93c0-4d4b-bc5b-fc80089e89cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fb029269-ed6a-4a76-8e74-9283b8d74e1f");
        }
    }
}
