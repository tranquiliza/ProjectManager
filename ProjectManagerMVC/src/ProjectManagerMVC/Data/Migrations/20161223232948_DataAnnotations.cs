using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagerMVC.Data.Migrations
{
    public partial class DataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status_Name",
                table: "Status",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Maintainance_Task",
                maxLength: 200,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Maintainance_Task",
                maxLength: 1000,
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletionDate",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "Maintainance_Task",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                maxLength: 200,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Business",
                maxLength: 200,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status_Name",
                table: "Status",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Maintainance_Task",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Maintainance_Task",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Maintainance_Task",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Maintainance_Task",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Maintainance_Task",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletionDate",
                table: "Maintainance_Task",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "Maintainance_Task",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Business",
                maxLength: 200,
                nullable: true);
        }
    }
}
