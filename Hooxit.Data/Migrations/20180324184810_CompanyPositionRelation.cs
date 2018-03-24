using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hooxit.Data.Migrations
{
    public partial class CompanyPositionRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Positions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CompanyID",
                table: "Positions",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Companies_CompanyID",
                table: "Positions",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Companies_CompanyID",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_CompanyID",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Positions");
        }
    }
}
