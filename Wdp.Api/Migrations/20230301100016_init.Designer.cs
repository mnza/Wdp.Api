// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wdp.Api;

#nullable disable

namespace Wdp.Api.Migrations
{
    [DbContext(typeof(WdpContext))]
    [Migration("20230301100016_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Wdp.Api.Models.BillDetail", b =>
                {
                    b.Property<Guid>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("detail_id");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("BillDateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("bill_date_time");

                    b.Property<Guid>("BillMasterBillId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("item_name");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("item_type");

                    b.Property<DateTime>("OperatorTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("operator_time")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<string>("Remark")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("DetailId");

                    b.HasIndex("BillMasterBillId");

                    b.ToTable("bill_detail", (string)null);
                });

            modelBuilder.Entity("Wdp.Api.Models.BillMaster", b =>
                {
                    b.Property<Guid>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("bill_id");

                    b.Property<DateTime>("BillDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("bill_date_time")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<double>("BillMoney")
                        .HasColumnType("double")
                        .HasColumnName("bill_money");

                    b.Property<string>("BillName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("bill_name");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BillId");

                    b.HasIndex("UserId");

                    b.ToTable("bill_master", (string)null);
                });

            modelBuilder.Entity("Wdp.Api.Models.FavWebsite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("fav_websites", (string)null);
                });

            modelBuilder.Entity("Wdp.Api.Models.OpConfig", b =>
                {
                    b.Property<int>("OperatorId")
                        .HasColumnType("int")
                        .HasColumnName("operator_id");

                    b.Property<string>("Category")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Key")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime>("OperatorTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("operator_time")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("OperatorId", "Category", "Key");

                    b.ToTable("op_config", (string)null);
                });

            modelBuilder.Entity("Wdp.Api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("IdNo")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("id_no");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone_number");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("user_name");

                    b.HasKey("UserId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Wdp.Api.Models.BillDetail", b =>
                {
                    b.HasOne("Wdp.Api.Models.BillMaster", "BillMaster")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillMasterBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillMaster");
                });

            modelBuilder.Entity("Wdp.Api.Models.BillMaster", b =>
                {
                    b.HasOne("Wdp.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Wdp.Api.Models.FavWebsite", b =>
                {
                    b.HasOne("Wdp.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Wdp.Api.Models.BillMaster", b =>
                {
                    b.Navigation("BillDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
