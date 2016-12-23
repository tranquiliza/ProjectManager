using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectManagerMVC.Data.Migrations
{
    public partial class Maintainance_Tasks_StatusFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Status_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Status_ID);
                });

            migrationBuilder.AddColumn<int>(
                name: "Status_ID",
                table: "Maintainance_Task",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
