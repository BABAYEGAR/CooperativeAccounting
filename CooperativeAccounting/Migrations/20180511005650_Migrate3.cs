using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CooperativeAccounting.Migrations
{
    public partial class Migrate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Urgent",
                table: "TransactionTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    BankId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Emergency = table.Column<bool>(nullable: false),
                    FirstGuarantorMobile = table.Column<string>(maxLength: 100, nullable: false),
                    FirstGuarantorName = table.Column<string>(maxLength: 100, nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    SecondGuarantorMobile = table.Column<string>(maxLength: 100, nullable: false),
                    SecondGuarantorName = table.Column<string>(maxLength: 100, nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionId = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AppUserId",
                table: "Loans",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BankId",
                table: "Loans",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_TransactionId",
                table: "Loans",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_TransactionTypeId",
                table: "Loans",
                column: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropColumn(
                name: "Urgent",
                table: "TransactionTypes");
        }
    }
}
