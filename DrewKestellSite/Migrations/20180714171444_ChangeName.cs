using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DrewKestellSite.Migrations
{
    public partial class ChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ArticleChapters",
                newName: "ChapterNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleChapters_ArticleId_Value",
                table: "ArticleChapters",
                newName: "IX_ArticleChapters_ArticleId_ChapterNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChapterNumber",
                table: "ArticleChapters",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleChapters_ArticleId_ChapterNumber",
                table: "ArticleChapters",
                newName: "IX_ArticleChapters_ArticleId_Value");
        }
    }
}
