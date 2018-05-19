using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CooperativeAccounting.Migrations
{
    public partial class Migrate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ManageCashTransaction",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManageLoan",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManageMinute",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManageWelfare",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Welfares",
                columns: table => new
                {
                    WelfareId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    Member = table.Column<bool>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TerminalDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Welfares", x => x.WelfareId);
                    table.ForeignKey(
                        name: "FK_Welfares_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Welfares_AppUserId",
                table: "Welfares",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Welfares");

            migrationBuilder.DropColumn(
                name: "ManageCashTransaction",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ManageLoan",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ManageMinute",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ManageWelfare",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "AppUsers");
        }
    }
}
