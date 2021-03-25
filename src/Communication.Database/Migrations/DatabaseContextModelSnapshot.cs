﻿// <auto-generated />
using DingDong.Backend.Communication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DingDong.Backend.Communication.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("DingDong.Backend.Common.Data.BadgeRegister", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsSet")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(767)");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("BadgeRegister");
                });

            modelBuilder.Entity("DingDong.Backend.Common.Data.User", b =>
                {
                    b.Property<string>("HashedKey")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.HasKey("HashedKey");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DingDong.Backend.Common.Data.BadgeRegister", b =>
                {
                    b.HasOne("DingDong.Backend.Common.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
