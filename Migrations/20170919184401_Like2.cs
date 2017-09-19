using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hello.Migrations
{
    public partial class Like2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Post_PostId1",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_PostId1",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "Like");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Like",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                table: "Like",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Post_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Post_PostId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_PostId",
                table: "Like");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "Like",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "Like",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId1",
                table: "Like",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Post_PostId1",
                table: "Like",
                column: "PostId1",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
