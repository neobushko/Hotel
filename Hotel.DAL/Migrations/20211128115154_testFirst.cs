using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.DAL.Migrations
{
    public partial class testFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "Rooms",
            type: "bit",
            nullable: false,
            defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "IsActive",
            table: "Rooms");
        }
    }
}
