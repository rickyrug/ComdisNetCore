﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Comdis.Migrations
{
    [DbContext(typeof(ComdisContext))]
    partial class ComdisContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Comdis.Comdis.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("longtext");

                    b.Property<string>("Adress2")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone1")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone2")
                        .HasColumnType("longtext");

                    b.Property<string>("RFC")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Comdis.Comdis.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("longtext");

                    b.Property<string>("Adress2")
                        .HasColumnType("longtext");

                    b.Property<string>("BankAccount")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone1")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone2")
                        .HasColumnType("longtext");

                    b.Property<string>("RFC")
                        .HasColumnType("longtext");

                    b.Property<int?>("SuscribedBankId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SuscribedBankId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Comdis.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Comdis.Models.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("code")
                        .HasColumnType("longtext");

                    b.Property<string>("value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("Comdis.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("UomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<int?>("categoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UomId");

                    b.HasIndex("categoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Comdis.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryCode")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Comdis.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasColumnType("longtext");

                    b.Property<string>("DeliveryAdress")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RequestedDeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("VendorId")
                        .HasColumnType("int");

                    b.Property<decimal>("discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("discount2")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("discount3")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("tax")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("VendorId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("Comdis.Models.PurchaseItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("POHeaderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("POHeaderId");

                    b.HasIndex("ProductId");

                    b.ToTable("PurchaseItems");
                });

            modelBuilder.Entity("Comdis.Models.Sales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeliveryAdress")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RequestedDeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SalesToPartyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<decimal>("discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("discount2")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("discount3")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("tax")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("SalesToPartyId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Comdis.Models.SalesItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("SalesHeaderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesHeaderId");

                    b.ToTable("SalesItems");
                });

            modelBuilder.Entity("Comdis.Models.UOM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Cretead")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

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

            modelBuilder.Entity("Comdis.Models.Purchase", b =>
                {
                    b.HasOne("Comdis.Comdis.Models.Supplier", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Comdis.Models.PurchaseItems", b =>
                {
                    b.HasOne("Comdis.Models.Purchase", "POHeader")
                        .WithMany("PurchaseItems")
                        .HasForeignKey("POHeaderId");

                    b.HasOne("Comdis.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("POHeader");

                    b.Navigation("Product");
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

            modelBuilder.Entity("Comdis.Models.Purchase", b =>
                {
                    b.Navigation("PurchaseItems");
                });

            modelBuilder.Entity("Comdis.Models.Sales", b =>
                {
                    b.Navigation("SalesItems");
                });
#pragma warning restore 612, 618
        }
    }
}
