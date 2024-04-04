﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletWatchAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryTypeMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");
        }
    }
}
