using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace shotnet.Migrations
{
    public partial class dateCreatedToInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_userId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_Invoice_invoiceId",
                table: "stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_userId",
                table: "invoices",
                newName: "IX_invoices_userId");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateCreated",
                table: "invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_invoices",
                table: "invoices",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "83b341c7-44a0-45aa-820b-ed3e7bfadc2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "40d3a446-ddf1-4588-a291-b103438c730a");

            migrationBuilder.AddForeignKey(
                name: "FK_invoices_AspNetUsers_userId",
                table: "invoices",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stock_invoices_invoiceId",
                table: "stock",
                column: "invoiceId",
                principalTable: "invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoices_AspNetUsers_userId",
                table: "invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_invoices_invoiceId",
                table: "stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_invoices",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "dateCreated",
                table: "invoices");

            migrationBuilder.RenameTable(
                name: "invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_invoices_userId",
                table: "Invoice",
                newName: "IX_Invoice_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_userId",
                table: "Invoice",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stock_Invoice_invoiceId",
                table: "stock",
                column: "invoiceId",
                principalTable: "Invoice",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
