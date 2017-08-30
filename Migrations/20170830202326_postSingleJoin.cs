using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hello.Migrations
{
    public partial class postSingleJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistPost");

            migrationBuilder.DropTable(
                name: "UserPost");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ApplicationArtist",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationArtistId",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Post",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_ApplicationArtistId",
                table: "Post",
                column: "ApplicationArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_ApplicationUserId",
                table: "Post",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_ApplicationArtist_ApplicationArtistId",
                table: "Post",
                column: "ApplicationArtistId",
                principalTable: "ApplicationArtist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_ApplicationUserId",
                table: "Post",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_ApplicationArtist_ApplicationArtistId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_ApplicationUserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_ApplicationArtistId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_ApplicationUserId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "ApplicationArtistId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUsers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApplicationArtist",
                newName: "ArtistId");

            migrationBuilder.CreateTable(
                name: "ArtistPost",
                columns: table => new
                {
                    ArtistId = table.Column<string>(nullable: false),
                    ArtistId1 = table.Column<int>(nullable: true),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistPost", x => x.ArtistId);
                    table.ForeignKey(
                        name: "FK_ArtistPost_ApplicationArtist_ArtistId1",
                        column: x => x.ArtistId1,
                        principalTable: "ApplicationArtist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtistPost_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPost",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    ApplicationArtistId = table.Column<int>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPost", x => new { x.UserId, x.PostId });
                    table.UniqueConstraint("AK_UserPost_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserPost_ApplicationArtist_ApplicationArtistId",
                        column: x => x.ApplicationArtistId,
                        principalTable: "ApplicationArtist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPost_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPost_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistPost_ArtistId1",
                table: "ArtistPost",
                column: "ArtistId1");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistPost_PostId",
                table: "ArtistPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_ApplicationArtistId",
                table: "UserPost",
                column: "ApplicationArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_PostId",
                table: "UserPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_UserId1",
                table: "UserPost",
                column: "UserId1");
        }
    }
}
