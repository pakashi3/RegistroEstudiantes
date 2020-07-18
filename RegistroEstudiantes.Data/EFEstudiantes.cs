using RegistroEstudiantes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistroEstudiantes.Data
{
    public class EFEstudiantes : IEstudianteService
    {
        private RegistroEstudiantesContext db;
        private string texto;

        public EFEstudiantes(RegistroEstudiantesContext db)
        {
            this.db = db;
        }
        public Estudiante ActualizarEstudiante(Estudiante estudianteActualizado)
        {
            var estudianteExistente = db.Estudiantes.SingleOrDefault(e => e.Id == estudianteActualizado.Id);
            estudianteExistente.Matricula = estudianteActualizado.Matricula;
            estudianteExistente.Nombre = estudianteActualizado.Nombre;
            estudianteExistente.Apellido = estudianteActualizado.Apellido;
            estudianteExistente.FechaNac = estudianteActualizado.FechaNac;
            estudianteExistente.Edad = estudianteActualizado.Edad;
            estudianteExistente.Meta = estudianteActualizado.Meta;
            estudianteExistente.Sexo = estudianteActualizado.Sexo;
            estudianteExistente.Carrera = estudianteActualizado.Carrera;

            return estudianteExistente;
        }

        public Estudiante CrearEstudiante(Estudiante estudiante)
        {
            db.Estudiantes.Add(estudiante);

            return estudiante;
        }

        public Estudiante Eliminar(int id)
        {
            var estudiante = db.Estudiantes.Single(e => e.Id == id);
            db.Estudiantes.Remove(estudiante);
            return estudiante;
        }

        public Estudiante GetEstudiantesPorId(int Id)
        {
            return this.db.Estudiantes.SingleOrDefault(n => n.Id == Id);
        }

        public IList<Estudiante> GetEstudiantesPorNombre(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.ToLower();
            }
            return db.Estudiantes.Where(e => string.IsNullOrEmpty(texto) || e.Nombre.ToLower().Contains(texto)).OrderBy(e => e.Nombre).ToList();
        }

        public object GetTotalEstudaintesRegistrados()
        {
            return db.Estudiantes.Count();
        }

        public int GuardarCambios()
        {
            return db.SaveChanges();
        }
    }
}
