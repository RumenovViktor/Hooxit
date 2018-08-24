using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hooxit.Data.Migrations
{
    public partial class addPositionCandidateManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PositionsCandidates",
                columns: table => new
                {
                    PositionId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionsCandidates", x => new { x.PositionId, x.CandidateId });
                    table.ForeignKey(
                        name: "FK_PositionsCandidates_Positions_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionsCandidates_Candidates_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionsCandidates_CandidateId",
                table: "PositionsCandidates",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionsCandidates");
        }
    }
}
