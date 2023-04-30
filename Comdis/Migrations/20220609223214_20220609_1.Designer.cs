﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Comdis.Migrations
{
    [DbContext(typeof(ComdisContext))]
    [Migration("20220609223214_20220609_1")]
    partial class _20220609_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("Comdis.Comdis.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress2")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone2")
                        .HasColumnType("TEXT");

                    b.Property<string>("RFC")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Comdis.Comdis.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress2")
                        .HasColumnType("TEXT");

                    b.Property<string>("BankAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone2")
                        .HasColumnType("TEXT");

                    b.Property<string>("RFC")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SuscribedBankId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SuscribedBankId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Comdis.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Comdis.Models.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<string>("value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("Comdis.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int?>("categoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UomId");

                    b.HasIndex("categoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Comdis.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Comdis.Models.Sales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeliveryAdress")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RequestedDeliveryDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SalesToPartyId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("discount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("discount2")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("discount3")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("tax")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SalesToPartyId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Comdis.Models.SalesItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SalesHeaderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesHeaderId");

                    b.ToTable("SalesItems");
                });

            modelBuilder.Entity("Comdis.Models.UOM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UOM");
                });

            modelBuilder.Entity("Comdis.Comdis.Models.Supplier", b =>
                {
                    b.HasOne("Comdis.Models.Bank", "SuscribedBank")
                        .WithMany()
                        .HasForeignKey("SuscribedBankId");

                    b.Navigation("SuscribedBank");
                });

            modelBuilder.Entity("Comdis.Models.Product", b =>
                {
                    b.HasOne("Comdis.Models.UOM", "Uom")
                        .WithMany()
                        .HasForeignKey("UomId");

                    b.HasOne("Comdis.Models.ProductCategory", "category")
                        .WithMany()
                        .HasForeignKey("categoryId");

                    b.Navigation("Uom");

                    b.Navigation("category");
                });

            modelBuilder.Entity("Comdis.Models.Sales", b =>
                {
                    b.HasOne("Comdis.Comdis.Models.Customer", "SalesToParty")
                        .WithMany()
                        .HasForeignKey("SalesToPartyId");

                    b.Navigation("SalesToParty");
                });

            modelBuilder.Entity("Comdis.Models.SalesItems", b =>
                {
                    b.HasOne("Comdis.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("Comdis.Models.Sales", "SalesHeader")
                        .WithMany("SalesItems")
                        .HasForeignKey("SalesHeaderId");

                    b.Navigation("Product");

                    b.Navigation("SalesHeader");
                });

            modelBuilder.Entity("Comdis.Models.Sales", b =>
                {
                    b.Navigation("SalesItems");
                });
#pragma warning restore 612, 618
        }
    }
}
