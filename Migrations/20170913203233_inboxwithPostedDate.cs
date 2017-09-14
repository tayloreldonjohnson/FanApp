using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hello.Migrations
{
    public partial class inboxwithPostedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessagerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RecieverOfMessageId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inbox_AspNetUsers_MessagerUserId",
                        column: x => x.MessagerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inbox_AspNetUsers_RecieverOfMessageId",
                        column: x => x.RecieverOfMessageId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_MessagerUserId",
                table: "Inbox",
                column: "MessagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_RecieverOfMessageId",
                table: "Inbox",
                column: "RecieverOfMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inbox");
        }
    }
}
