﻿// <auto-generated />
using System;
using ArdantOffical.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArdantOffical.Migrations
{
    [DbContext(typeof(FGCDbContext))]
    [Migration("20240424065014_CreateSignatureFieldsTblUser")]
    partial class CreateSignatureFieldsTblUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArdantOffical.Models.APIErrorLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tbl_APIErrorLog");
                });

            modelBuilder.Entity("ArdantOffical.Models.ApiCredential", b =>
                {
                    b.Property<int>("AuthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AuthID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CertPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertThumbprint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certificate")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Company")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Ip")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("IP");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Purpose")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("AuthId");

                    b.ToTable("ApiCredentials");
                });

            modelBuilder.Entity("ArdantOffical.Models.Attachments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Folder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SalesforceID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("ArdantOffical.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icons")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsParent")
                        .HasColumnType("bit");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("MenuItemParentID")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuItemID");

                    b.HasIndex("MenuItemParentID");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("ArdantOffical.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("MeaningfulName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            MeaningfulName = "Admin",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            IsDelete = false,
                            MeaningfulName = "Compliance",
                            Name = "Compliance"
                        },
                        new
                        {
                            Id = 3,
                            IsDelete = false,
                            MeaningfulName = "Auditor",
                            Name = "Auditor"
                        },
                        new
                        {
                            Id = 4,
                            IsDelete = false,
                            MeaningfulName = "Deputy Money Laundering Reporting Officer",
                            Name = "DMLRO"
                        },
                        new
                        {
                            Id = 5,
                            IsDelete = false,
                            MeaningfulName = "Money Laundering Reporting Officer",
                            Name = "MLRO"
                        },
                        new
                        {
                            Id = 6,
                            IsDelete = false,
                            MeaningfulName = "Finance",
                            Name = "Finance"
                        },
                        new
                        {
                            Id = 7,
                            IsDelete = false,
                            MeaningfulName = "Internee",
                            Name = "Internee"
                        },
                        new
                        {
                            Id = 8,
                            IsDelete = false,
                            MeaningfulName = "Business Relationship",
                            Name = "Business Relationship"
                        },
                        new
                        {
                            Id = 9,
                            IsDelete = false,
                            MeaningfulName = "Operations",
                            Name = "Operations"
                        },
                        new
                        {
                            Id = 10,
                            IsDelete = false,
                            MeaningfulName = "On-Boarding",
                            Name = "On-Boarding"
                        },
                        new
                        {
                            Id = 11,
                            IsDelete = false,
                            MeaningfulName = "Compliance Commitee",
                            Name = "Compliance Commitee"
                        });
                });

            modelBuilder.Entity("ArdantOffical.Models.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Tbl_RoleClaim");
                });

            modelBuilder.Entity("ArdantOffical.Models.TblUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime");

                    b.Property<string>("Designation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EnableTwoFactor")
                        .HasColumnType("bit");

                    b.Property<string>("Firstname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Folder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OnlineStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordReset")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SalesforceID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignaturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignatureTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SkipAuthenticator")
                        .HasColumnType("bit");

                    b.Property<string>("UserKey")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserRole")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Admin,Compliance,Auditor");

                    b.Property<int?>("UserStatus")
                        .HasColumnType("int")
                        .HasComment("0=Blocked, 1=Approved/Allowed");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Tbl_Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Designation = "Administrator",
                            Email = "jsmith@gmail.com",
                            EnableTwoFactor = false,
                            Firstname = "JOHN",
                            IsDelete = false,
                            Lastname = "SMITH",
                            Password = "admin",
                            SkipAuthenticator = false,
                            UserKey = "0a2w38de90123",
                            UserStatus = 1,
                            Username = "jsmith@gmail.com"
                        });
                });

            modelBuilder.Entity("ArdantOffical.Models.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IntroducerUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("MenuItemParentID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersUserId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("ArdantOffical.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("Tbl_UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            RoleId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("ArdantOffical.Models.MenuItem", b =>
                {
                    b.HasOne("ArdantOffical.Models.MenuItem", "MenuItems")
                        .WithMany("MenuItemChild")
                        .HasForeignKey("MenuItemParentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("ArdantOffical.Models.RoleClaim", b =>
                {
                    b.HasOne("ArdantOffical.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ArdantOffical.Models.UserClaim", b =>
                {
                    b.HasOne("ArdantOffical.Models.TblUser", "Users")
                        .WithMany("UserClaims")
                        .HasForeignKey("UsersUserId");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ArdantOffical.Models.UserRole", b =>
                {
                    b.HasOne("ArdantOffical.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArdantOffical.Models.TblUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArdantOffical.Models.MenuItem", b =>
                {
                    b.Navigation("MenuItemChild");
                });

            modelBuilder.Entity("ArdantOffical.Models.TblUser", b =>
                {
                    b.Navigation("UserClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
