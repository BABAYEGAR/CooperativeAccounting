using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CooperativeAccounting.Migrations
{
    public partial class Migrate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Lgas",
                columns: table => new
                {
                    LgaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lgas", x => x.LgaId);
                });

            migrationBuilder.CreateTable(
                name: "Minutes",
                columns: table => new
                {
                    MinuteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    Text = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minutes", x => x.MinuteId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    ManageAllTransaction = table.Column<bool>(nullable: false),
                    ManageCashTransaction = table.Column<bool>(nullable: false),
                    ManageLoan = table.Column<bool>(nullable: false),
                    ManageMemberRoles = table.Column<bool>(nullable: false),
                    ManageMemberTransaction = table.Column<bool>(nullable: false),
                    ManageMembers = table.Column<bool>(nullable: false),
                    ManageMinute = table.Column<bool>(nullable: false),
                    ManageTransactionType = table.Column<bool>(nullable: false),
                    ManageWelfare = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    TransactionTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Asset = table.Column<bool>(nullable: false),
                    Cash = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    Credit = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    Debit = table.Column<bool>(nullable: false),
                    Equity = table.Column<bool>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    Liability = table.Column<bool>(nullable: false),
                    Loan = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Urgent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.TransactionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    BackgroundPicture = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FileNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    LgaId = table.Column<int>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 100, nullable: true),
                    MobileExtension = table.Column<string>(nullable: true),
                    MonthlyContribution = table.Column<double>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    NextOfKin = table.Column<string>(nullable: false),
                    NextOfKinAddress = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    RoleId = table.Column<long>(nullable: false),
                    Spouce = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_AppUsers_Lgas_LgaId",
                        column: x => x.LgaId,
                        principalTable: "Lgas",
                        principalColumn: "LgaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    AppUserId = table.Column<long>(nullable: false),
                    BankId = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionName = table.Column<string>(nullable: false),
                    TransactionTypeId = table.Column<long>(nullable: false),
                    VoucherNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Welfares",
                columns: table => new
                {
                    WelfareId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    AppUserId = table.Column<long>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
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
                    InterestRate = table.Column<double>(nullable: false),
                    LastModifiedBy = table.Column<long>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    SecondGuarantorMobile = table.Column<string>(maxLength: 100, nullable: false),
                    SecondGuarantorName = table.Column<string>(maxLength: 100, nullable: false),
                    TerminalDate = table.Column<DateTime>(nullable: false),
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
                name: "IX_AppUsers_LgaId",
                table: "AppUsers",
                column: "LgaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleId",
                table: "AppUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StateId",
                table: "AppUsers",
                column: "StateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AppUserId",
                table: "Transactions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankId",
                table: "Transactions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Welfares_AppUserId",
                table: "Welfares",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Minutes");

            migrationBuilder.DropTable(
                name: "Welfares");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "Lgas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
