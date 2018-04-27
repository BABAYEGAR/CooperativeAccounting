﻿// <auto-generated />
using CooperativeAccounting.Models.DataBaseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CooperativeAccounting.Migrations
{
    [DbContext(typeof(CooperativeAccountingDataContext))]
    [Migration("20180427010444_Migrate2")]
    partial class Migrate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("CooperativeAccounting.Models.Entities.AppUser", b =>
                {
                    b.Property<long>("AppUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BackgroundPicture");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired();

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastModified");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<long?>("LastModifiedBy");

                    b.Property<string>("Mobile")
                        .HasMaxLength(100);

                    b.Property<string>("MobileExtension");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("ProfilePicture");

                    b.Property<long?>("RoleId")
                        .IsRequired();

                    b.Property<string>("Status");

                    b.Property<string>("Website");

                    b.HasKey("AppUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("CooperativeAccounting.Models.Entities.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastModified");

                    b.Property<long?>("LastModifiedBy");

                    b.Property<bool>("ManageAllTransaction");

                    b.Property<bool>("ManageMemberRoles");

                    b.Property<bool>("ManageMemberTransaction");

                    b.Property<bool>("ManageMembers");

                    b.Property<bool>("ManageTransactionType");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CooperativeAccounting.Models.Entities.Transaction", b =>
                {
                    b.Property<long>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<double>("Amount");

                    b.Property<long>("AppUserId");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastModified");

                    b.Property<long?>("LastModifiedBy");

                    b.Property<DateTime?>("TransactionDate")
                        .IsRequired();

                    b.Property<string>("TransactionName")
                        .IsRequired();

                    b.Property<long>("TransactionTypeId");

                    b.Property<string>("VoucherNumber");

                    b.HasKey("TransactionId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CooperativeAccounting.Models.Entities.TransactionType", b =>
                {
                    b.Property<long>("TransactionTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Asset");

                    b.Property<bool>("Cash");

                    b.Property<long?>("CreatedBy");

                    b.Property<bool>("Credit");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastModified");

                    b.Property<bool>("Debit");

                    b.Property<bool>("Equity");

                    b.Property<long?>("LastModifiedBy");

                    b.Property<bool>("Liability");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TransactionTypeId");

                    b.ToTable("TransactionTypes");
                });

            modelBuilder.Entity("CooperativeAccounting.Models.Entities.AppUser", b =>
                {
                    b.HasOne("CooperativeAccounting.Models.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CooperativeAccounting.Models.Entities.Transaction", b =>
                {
                    b.HasOne("CooperativeAccounting.Models.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CooperativeAccounting.Models.Entities.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
