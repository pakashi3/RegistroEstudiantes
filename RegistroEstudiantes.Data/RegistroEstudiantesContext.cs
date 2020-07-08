using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RegistroEstudiantes.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroEstudiantes.Data
{
    public class RegistroEstudiantesContext : DbContext
    {
        public RegistroEstudiantesContext(DbContextOptions<RegistroEstudiantesContext> options) : base(options)
        {

        }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }

    }
}
