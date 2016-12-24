using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagerMVC.Data.Migrations
{
    public partial class EnsureNotNullStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.AlterColumn<int>(
                name: "Status_ID",
                table: "Maintainance_Task",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task",
                column: "Status_ID",
                principalTable: "Status",
                principalColumn: "Status_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Status_ID",
                table: "Maintainance_Task",
                column: "Status_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.AlterColumn<int>(
                name: "Status_ID",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task",
                column: "Status_ID",
                principalTable: "Status",
                principalColumn: "Status_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
