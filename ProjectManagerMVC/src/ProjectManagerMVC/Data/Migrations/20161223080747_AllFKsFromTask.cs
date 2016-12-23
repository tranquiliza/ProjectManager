using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectManagerMVC.Data.Migrations
{
    public partial class AllFKsFromTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Business_ID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Department_Business_Business_ID",
                        column: x => x.Business_ID,
                        principalTable: "Business",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Department_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Staff_Department_Department_ID",
                        column: x => x.Department_ID,
                        principalTable: "Department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "Business_ID",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Department_ID",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Maintask_ID",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Staff_ID",
                table: "Maintainance_Task",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Business_ID",
                table: "Maintainance_Task",
                column: "Business_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Department_ID",
                table: "Maintainance_Task",
                column: "Department_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Maintask_ID",
                table: "Maintainance_Task",
                column: "Maintask_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainance_Task_Staff_ID",
                table: "Maintainance_Task",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Business_ID",
                table: "Department",
                column: "Business_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Department_ID",
                table: "Staff",
                column: "Department_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Business_Business_ID",
                table: "Maintainance_Task",
                column: "Business_ID",
                principalTable: "Business",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Department_Department_ID",
                table: "Maintainance_Task",
                column: "Department_ID",
                principalTable: "Department",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainance_Task_Maintainance_Task_Maintask_ID",
                table: "Maintainance_Task",
                column: "Maintask_ID",
                principalTable: "Maintainance_Task",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Maintainance_Task_Business_Business_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Department_Department_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Maintainance_Task_Maintask_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Staff_Staff_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainance_Task_Status_Status_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Business_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Department_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Maintask_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropIndex(
                name: "IX_Maintainance_Task_Staff_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Business_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "Department_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "Maintask_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropColumn(
                name: "Staff_ID",
                table: "Maintainance_Task");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.AddColumn<int>(
                name: "Status_ID",
                table: "Status",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Status_Name",
                table: "Status",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Status_ID");

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
