using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectManagerMVC.Migrations
{
    public partial class StaffUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    HiredDate = table.Column<DateTime>(nullable: false),
                    Initials = table.Column<string>(maxLength: 5, nullable: false),
                    JobTitle = table.Column<string>(maxLength: 200, nullable: false),
                    MobilePhone = table.Column<string>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    Surname = table.Column<string>(maxLength: 200, nullable: false),
                    WorkPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.ID);
                });

            migrationBuilder.AddColumn<int>(
                name: "Staff_ID",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Staff_ID",
                table: "Maintainance_Task",
                column: "Staff_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Staff_Staff_ID",
                table: "Maintainance_Task",
                column: "Staff_ID",
                principalTable: "Staff",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Staff_Staff_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Staff_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "Staff_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
