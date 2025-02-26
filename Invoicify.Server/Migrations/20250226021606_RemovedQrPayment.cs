using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoicify.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovedQrPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankInfo_QRPayment_QRCodeId",
                table: "BankInfo");

            migrationBuilder.DropTable(
                name: "QRPayment");

            migrationBuilder.DropIndex(
                name: "IX_BankInfo_QRCodeId",
                table: "BankInfo");

            migrationBuilder.DropColumn(
                name: "QRCodeId",
                table: "BankInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QRCodeId",
                table: "BankInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QRPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Account = table.Column<string>(type: "TEXT", maxLength: 46, nullable: false),
                    AlternativeAccount = table.Column<string>(type: "TEXT", maxLength: 93, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    MessageForRecipient = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    RecipientName = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true),
                    VariableSymbol = table.Column<int>(type: "INTEGER", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QRPayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankInfo_QRCodeId",
                table: "BankInfo",
                column: "QRCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_QRPayment_InvoiceId",
                table: "QRPayment",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankInfo_QRPayment_QRCodeId",
                table: "BankInfo",
                column: "QRCodeId",
                principalTable: "QRPayment",
                principalColumn: "Id");
        }
    }
}
