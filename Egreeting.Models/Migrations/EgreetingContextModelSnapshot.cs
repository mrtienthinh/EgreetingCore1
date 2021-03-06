﻿// <auto-generated />
using System;
using Egreeting.Models.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egreeting.Models.Migrations
{
    [DbContext(typeof(EgreetingContext))]
    partial class EgreetingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Egreeting.Models.Models.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<int?>("EgreetingRoleID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EgreetingRoleID");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Egreeting.Models.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<int?>("EgreetingUserID")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EgreetingUserID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CategorySlug")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("CategoryID");

                    b.HasIndex("CategorySlug")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Egreeting.Models.Models.CategoryEcard", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("EcardId")
                        .HasColumnType("integer");

                    b.HasKey("CategoryId", "EcardId");

                    b.HasIndex("EcardId");

                    b.ToTable("CategoryEcard");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Ecard", b =>
                {
                    b.Property<int>("EcardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<string>("EcardName")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("EcardSlug")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<int>("EcardType")
                        .HasColumnType("integer");

                    b.Property<string>("EcardUrl")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<int?>("EgreetingUserID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("EcardID");

                    b.HasIndex("EcardSlug")
                        .IsUnique();

                    b.HasIndex("EgreetingUserID");

                    b.ToTable("Ecards");
                });

            modelBuilder.Entity("Egreeting.Models.Models.EgreetingRole", b =>
                {
                    b.Property<int>("EgreetingRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<string>("EgreetingRoleName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("EgreetingRoleID");

                    b.HasIndex("EgreetingRoleName")
                        .IsUnique();

                    b.ToTable("EgreetingRoles");
                });

            modelBuilder.Entity("Egreeting.Models.Models.EgreetingUser", b =>
                {
                    b.Property<int>("EgreetingUserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreditCardCVG")
                        .HasColumnType("character varying(3)")
                        .HasMaxLength(3);

                    b.Property<string>("CreditCardNumber")
                        .HasColumnType("character varying(12)")
                        .HasMaxLength(12);

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<string>("EgreetingUserSlug")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("PaymentDueDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("EgreetingUserID");

                    b.ToTable("EgreetingUsers");
                });

            modelBuilder.Entity("Egreeting.Models.Models.EgreetingUserRole", b =>
                {
                    b.Property<int>("EgreetingUserId")
                        .HasColumnType("integer");

                    b.Property<int>("EgreetingRoleId")
                        .HasColumnType("integer");

                    b.HasKey("EgreetingUserId", "EgreetingRoleId");

                    b.HasIndex("EgreetingRoleId");

                    b.ToTable("EgreetingUserRole");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<int?>("EcardID")
                        .HasColumnType("integer");

                    b.Property<int?>("EgreetingUserID")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Subject")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("FeedbackID");

                    b.HasIndex("EcardID");

                    b.HasIndex("EgreetingUserID");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RecipientEmail")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ScheduleTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SendMessage")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("SendStatus")
                        .HasColumnType("boolean");

                    b.Property<string>("SendSubject")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("SenderName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double precision");

                    b.Property<int?>("UserEgreetingUserID")
                        .HasColumnType("integer");

                    b.HasKey("OrderID");

                    b.HasIndex("UserEgreetingUserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Egreeting.Models.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<int?>("EcardID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OrderID")
                        .HasColumnType("integer");

                    b.Property<bool>("SendStatus")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("SendTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("EcardID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<int?>("EgreetingUserID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("boolean");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("PaymentID");

                    b.HasIndex("EgreetingUserID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Egreeting.Models.Models.ScheduleSender", b =>
                {
                    b.Property<int>("ScheduleSenderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<int?>("EcardID")
                        .HasColumnType("integer");

                    b.Property<int?>("EgreetingUserID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RecipientEmail")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ScheduleTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ScheduleType")
                        .HasColumnType("integer");

                    b.Property<string>("SendMessage")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SendSubject")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("SenderName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("ScheduleSenderID");

                    b.HasIndex("EcardID");

                    b.HasIndex("EgreetingUserID");

                    b.ToTable("ScheduleSenders");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Subcriber", b =>
                {
                    b.Property<int>("SubcriberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("Draft")
                        .HasColumnType("boolean");

                    b.Property<int>("EgreetingUserID")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("SubcriberID");

                    b.HasIndex("EgreetingUserID")
                        .IsUnique();

                    b.ToTable("Subcribers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Egreeting.Models.Models.ApplicationRole", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingRole", "EgreetingRole")
                        .WithMany()
                        .HasForeignKey("EgreetingRoleID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.ApplicationUser", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithMany()
                        .HasForeignKey("EgreetingUserID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.CategoryEcard", b =>
                {
                    b.HasOne("Egreeting.Models.Models.Category", "Category")
                        .WithMany("CategoryEcards")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Egreeting.Models.Models.Ecard", "Ecard")
                        .WithMany("CategoryEcards")
                        .HasForeignKey("EcardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Egreeting.Models.Models.Ecard", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithMany("Ecards")
                        .HasForeignKey("EgreetingUserID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.EgreetingUserRole", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingRole", "EgreetingRole")
                        .WithMany("EgreetingUserRoles")
                        .HasForeignKey("EgreetingRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithMany("EgreetingUserRoles")
                        .HasForeignKey("EgreetingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Egreeting.Models.Models.Feedback", b =>
                {
                    b.HasOne("Egreeting.Models.Models.Ecard", "Ecard")
                        .WithMany()
                        .HasForeignKey("EcardID");

                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithMany("Feedbacks")
                        .HasForeignKey("EgreetingUserID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Order", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "User")
                        .WithMany()
                        .HasForeignKey("UserEgreetingUserID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.OrderDetail", b =>
                {
                    b.HasOne("Egreeting.Models.Models.Ecard", "Ecard")
                        .WithMany()
                        .HasForeignKey("EcardID");

                    b.HasOne("Egreeting.Models.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Payment", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithMany()
                        .HasForeignKey("EgreetingUserID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.ScheduleSender", b =>
                {
                    b.HasOne("Egreeting.Models.Models.Ecard", "Ecard")
                        .WithMany()
                        .HasForeignKey("EcardID");

                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithMany("ScheduleSenders")
                        .HasForeignKey("EgreetingUserID");
                });

            modelBuilder.Entity("Egreeting.Models.Models.Subcriber", b =>
                {
                    b.HasOne("Egreeting.Models.Models.EgreetingUser", "EgreetingUser")
                        .WithOne("Subcriber")
                        .HasForeignKey("Egreeting.Models.Models.Subcriber", "EgreetingUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Egreeting.Models.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Egreeting.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Egreeting.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Egreeting.Models.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Egreeting.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Egreeting.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
