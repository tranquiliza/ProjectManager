using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagerMVC.Migrations
{
    public partial class StatusRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Maintainance_Task",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_StatusId",
                table: "Maintainance_Task",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Status_StatusId",
                table: "Maintainance_Task",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Status_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Status_StatusId",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_StatusId",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Maintainance_Task");

            migrationBuilder.AddColumn<int>(
                name: "Status_ID",
                table: "Maintainance_Task",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Status_ID",
                table: "Maintainance_Task",
                column: "Status_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task",
                column: "Status_ID",
                principalTable: "Status",
                principalColumn: "Status_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
