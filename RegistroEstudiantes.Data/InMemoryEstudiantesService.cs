using System;
using System.Collections.Generic;
using RegistroEstudiantes.Model;
using System.Linq;

namespace RegistroEstudiantes.Data
{
    public class InMemoryEstudiantesService : IEstudianteService
    {
        IList<Estudiante> Estudiantes;

        public InMemoryEstudiantesService()
        {
            //comentario

            this.Estudiantes = new List<Estudiante>()
                {
                   new Estudiante { Id = 1, Matricula = "2145004", Nombre = "Pablo Rafael", Apellido = "Perez Volquez", Edad = 29, Meta = "Ser un gran Desarrollador de Aplicaciones, para todas las Plataformas"},
                   new Estudiante { Id = 2, Matricula = "2145014", Nombre = "Miguel Alexader", Apellido = "Perez Volquez", Edad = 22, Meta = "Ser el Deportista #1 del Mundo"},
                   new Estudiante { Id = 3, Matricula = "2145005", Nombre = "Wilmer", Apellido = "Perez Matos", Edad = 28, Meta = "Ser un Pelotero de Grandes Ligas"},
                   new Estudiante { Id = 4, Matricula = "2145023", Nombre = "Wilkin", Apellido = "Perez Matos", Edad = 23, Meta = "Ser Jugador de Video Juegos  Profesional"},
                   new Estudiante { Id = 5, Matricula = "2145224", Nombre = "Aurel Mauricia", Apellido = "Leyba Savinon", Edad = 32, Meta = "Ser Maestra"},
                };
        }

        public Estudiante ActualizarEstudiante(Estudiante estudianteActualizado)
        {
            var estudianteExistente = Estudiantes.SingleOrDefault(e => e.Id == estudianteActualizado.Id);
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
            estudiante.Id = Estudiantes.Max(e => e.Id) + 1;

            Estudiantes.Add(estudiante);

            return estudiante;
        }

        public Estudiante Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Estudiante GetEstudiantesPorId(int Id)
        {
            return this.Estudiantes.SingleOrDefault(n => n.Id == Id);
        }

        public IList<Estudiante> GetEstudiantesPorNombre(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.ToLower();
            }
            return Estudiantes.Where(e => string.IsNullOrEmpty(texto) || e.Nombre.ToLower().Contains(texto)).OrderBy(e => e.Nombre).ToList();
        }

        public object GetTotalEstudaintesRegistrados()
        {
            throw new NotImplementedException();
        }

        public int GuardarCambios()
        {
            return 1;
        }
    }
}

