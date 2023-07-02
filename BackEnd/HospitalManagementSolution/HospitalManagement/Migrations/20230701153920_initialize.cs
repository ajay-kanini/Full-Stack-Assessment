using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Patients",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Doctors",
                newName: "age");

            migrationBuilder.AlterColumn<int>(
                name: "age",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "age",
                table: "Patients",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Doctors",
                newName: "Age");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
