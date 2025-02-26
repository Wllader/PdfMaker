using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoicify.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankInfo_Invoices_InvoiceId",
                table: "BankInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoices_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PartyInfo_CustomerInfoId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PartyInfo_SellerInfoId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_Invoices_InvoiceId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_QRPayment_Invoices_InvoiceId",
                table: "QRPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SellerInfoId",
                table: "Invoice",
                newName: "IX_Invoice_SellerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CustomerInfoId",
                table: "Invoice",
                newName: "IX_Invoice_CustomerInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankInfo_Invoice_InvoiceId",
                table: "BankInfo",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_PartyInfo_CustomerInfoId",
                table: "Invoice",
                column: "CustomerInfoId",
                principalTable: "PartyInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_PartyInfo_SellerInfoId",
                table: "Invoice",
                column: "SellerInfoId",
                principalTable: "PartyInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_Invoice_InvoiceId",
                table: "OrderInfo",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QRPayment_Invoice_InvoiceId",
                table: "QRPayment",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankInfo_Invoice_InvoiceId",
                table: "BankInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_PartyInfo_CustomerInfoId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_PartyInfo_SellerInfoId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_Invoice_InvoiceId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_QRPayment_Invoice_InvoiceId",
                table: "QRPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_SellerInfoId",
                table: "Invoices",
                newName: "IX_Invoices_SellerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_CustomerInfoId",
                table: "Invoices",
                newName: "IX_Invoices_CustomerInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankInfo_Invoices_InvoiceId",
                table: "BankInfo",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoices_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PartyInfo_CustomerInfoId",
                table: "Invoices",
                column: "CustomerInfoId",
                principalTable: "PartyInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PartyInfo_SellerInfoId",
                table: "Invoices",
                column: "SellerInfoId",
                principalTable: "PartyInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_Invoices_InvoiceId",
                table: "OrderInfo",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QRPayment_Invoices_InvoiceId",
                table: "QRPayment",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
