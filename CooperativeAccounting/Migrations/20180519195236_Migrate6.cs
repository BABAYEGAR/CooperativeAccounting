using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CooperativeAccounting.Migrations
{
    public partial class Migrate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Welfares_AppUsers_AppUserId",
                table: "Welfares");

            migrationBuilder.AlterColumn<long>(
                name: "AppUserId",
                table: "Welfares",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "Transactions",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankId",
                table: "Transactions",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Banks_BankId",
                table: "Transactions",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Welfares_AppUsers_AppUserId",
                table: "Welfares",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Banks_BankId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Welfares_AppUsers_AppUserId",
                table: "Welfares");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Transactions");

            migrationBuilder.AlterColumn<long>(
                name: "AppUserId",
                table: "Welfares",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Welfares_AppUsers_AppUserId",
                table: "Welfares",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
