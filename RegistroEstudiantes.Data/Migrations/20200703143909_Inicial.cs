using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroEstudiantes.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(maxLength: 7, nullable: false),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Apellido = table.Column<string>(maxLength: 255, nullable: true),
                    FechaNac = table.Column<DateTime>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    Meta = table.Column<string>(maxLength: 1000, nullable: false),
                    Carrera = table.Column<int>(nullable: false),
                    Sexo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Codigo = table.Column<string>(maxLength: 4, nullable: false),
                    Area = table.Column<int>(nullable: false),
                    Disponible = table.Column<bool>(nullable: false),
                    Objetivos = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}
