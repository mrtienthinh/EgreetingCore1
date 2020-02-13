using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Egreeting.Models.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    CategorySlug = table.Column<string>(maxLength: 100, nullable: true),
                    CategoryName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "EgreetingRoles",
                columns: table => new
                {
                    EgreetingRoleID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    EgreetingRoleName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgreetingRoles", x => x.EgreetingRoleID);
                });

            migrationBuilder.CreateTable(
                name: "EgreetingUsers",
                columns: table => new
                {
                    EgreetingUserID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    EgreetingUserSlug = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Avatar = table.Column<byte[]>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    CreditCardNumber = table.Column<string>(maxLength: 12, nullable: true),
                    CreditCardCVG = table.Column<string>(maxLength: 3, nullable: true),
                    PaymentDueDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgreetingUsers", x => x.EgreetingUserID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ecards",
                columns: table => new
                {
                    EcardID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    EcardName = table.Column<string>(maxLength: 150, nullable: false),
                    EcardSlug = table.Column<string>(maxLength: 200, nullable: true),
                    EcardType = table.Column<int>(nullable: false),
                    EcardUrl = table.Column<string>(maxLength: 150, nullable: true),
                    ThumbnailUrl = table.Column<string>(maxLength: 150, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    EgreetingUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecards", x => x.EcardID);
                    table.ForeignKey(
                        name: "FK_Ecards_EgreetingUsers_EgreetingUserID",
                        column: x => x.EgreetingUserID,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EgreetingUserRole",
                columns: table => new
                {
                    EgreetingUserId = table.Column<int>(nullable: false),
                    EgreetingRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgreetingUserRole", x => new { x.EgreetingUserId, x.EgreetingRoleId });
                    table.ForeignKey(
                        name: "FK_EgreetingUserRole_EgreetingRoles_EgreetingRoleId",
                        column: x => x.EgreetingRoleId,
                        principalTable: "EgreetingRoles",
                        principalColumn: "EgreetingRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EgreetingUserRole_EgreetingUsers_EgreetingUserId",
                        column: x => x.EgreetingUserId,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    SenderName = table.Column<string>(maxLength: 100, nullable: true),
                    RecipientEmail = table.Column<string>(maxLength: 100, nullable: true),
                    SendSubject = table.Column<string>(maxLength: 100, nullable: true),
                    SendMessage = table.Column<string>(maxLength: 500, nullable: true),
                    ScheduleTime = table.Column<DateTime>(nullable: true),
                    SendStatus = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    UserEgreetingUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_EgreetingUsers_UserEgreetingUserID",
                        column: x => x.UserEgreetingUserID,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<bool>(nullable: false),
                    EgreetingUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_EgreetingUsers_EgreetingUserID",
                        column: x => x.EgreetingUserID,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subcribers",
                columns: table => new
                {
                    SubcriberID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    EgreetingUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcribers", x => x.SubcriberID);
                    table.ForeignKey(
                        name: "FK_Subcribers_EgreetingUsers_EgreetingUserID",
                        column: x => x.EgreetingUserID,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEcard",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    EcardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEcard", x => new { x.CategoryId, x.EcardId });
                    table.ForeignKey(
                        name: "FK_CategoryEcard_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEcard_Ecards_EcardId",
                        column: x => x.EcardId,
                        principalTable: "Ecards",
                        principalColumn: "EcardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    EcardID = table.Column<int>(nullable: true),
                    EgreetingUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Ecards_EcardID",
                        column: x => x.EcardID,
                        principalTable: "Ecards",
                        principalColumn: "EcardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedbacks_EgreetingUsers_EgreetingUserID",
                        column: x => x.EgreetingUserID,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleSenders",
                columns: table => new
                {
                    ScheduleSenderID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    ScheduleTime = table.Column<DateTime>(nullable: true),
                    ScheduleType = table.Column<int>(nullable: false),
                    SenderName = table.Column<string>(maxLength: 100, nullable: true),
                    RecipientEmail = table.Column<string>(maxLength: 100, nullable: true),
                    SendSubject = table.Column<string>(maxLength: 100, nullable: true),
                    SendMessage = table.Column<string>(maxLength: 500, nullable: true),
                    EcardID = table.Column<int>(nullable: true),
                    EgreetingUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSenders", x => x.ScheduleSenderID);
                    table.ForeignKey(
                        name: "FK_ScheduleSenders_Ecards_EcardID",
                        column: x => x.EcardID,
                        principalTable: "Ecards",
                        principalColumn: "EcardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleSenders_EgreetingUsers_EgreetingUserID",
                        column: x => x.EgreetingUserID,
                        principalTable: "EgreetingUsers",
                        principalColumn: "EgreetingUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Draft = table.Column<bool>(nullable: true),
                    SendStatus = table.Column<bool>(nullable: false),
                    SendTime = table.Column<DateTime>(nullable: true),
                    EcardID = table.Column<int>(nullable: true),
                    OrderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Ecards_EcardID",
                        column: x => x.EcardID,
                        principalTable: "Ecards",
                        principalColumn: "EcardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategorySlug",
                table: "Categories",
                column: "CategorySlug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEcard_EcardId",
                table: "CategoryEcard",
                column: "EcardId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecards_EcardSlug",
                table: "Ecards",
                column: "EcardSlug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ecards_EgreetingUserID",
                table: "Ecards",
                column: "EgreetingUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EgreetingRoles_EgreetingRoleName",
                table: "EgreetingRoles",
                column: "EgreetingRoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EgreetingUserRole_EgreetingRoleId",
                table: "EgreetingUserRole",
                column: "EgreetingRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EcardID",
                table: "Feedbacks",
                column: "EcardID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EgreetingUserID",
                table: "Feedbacks",
                column: "EgreetingUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_EcardID",
                table: "OrderDetails",
                column: "EcardID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserEgreetingUserID",
                table: "Orders",
                column: "UserEgreetingUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EgreetingUserID",
                table: "Payments",
                column: "EgreetingUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSenders_EcardID",
                table: "ScheduleSenders",
                column: "EcardID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSenders_EgreetingUserID",
                table: "ScheduleSenders",
                column: "EgreetingUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Subcribers_EgreetingUserID",
                table: "Subcribers",
                column: "EgreetingUserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategoryEcard");

            migrationBuilder.DropTable(
                name: "EgreetingUserRole");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ScheduleSenders");

            migrationBuilder.DropTable(
                name: "Subcribers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "EgreetingRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ecards");

            migrationBuilder.DropTable(
                name: "EgreetingUsers");
        }
    }
}
