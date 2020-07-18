using RegistroEstudiantes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistroEstudiantes.Data
{
    public class EFMaterias : IMateriaService
    {
        private RegistroEstudiantesContext db;
        public EFMaterias(RegistroEstudiantesContext db)
        {
            this.db = db;
        }
        public Materia ActualizarMateria(Materia materiaActualizada)
        {
            var materiaExistente = db.Materias.SingleOrDefault(m => m.Id == materiaActualizada.Id);
            materiaExistente.Nombre = materiaActualizada.Nombre;
            materiaExistente.Codigo = materiaActualizada.Codigo;
            materiaExistente.Objetivos = materiaActualizada.Objetivos;
            materiaExistente.Disponible = materiaActualizada.Disponible;
            materiaExistente.Area = materiaActualizada.Area;

            return materiaExistente;
        }

        public Materia CrearMateria(Materia materia)
        {
           db.Materias.Add(materia);

            return materia;
        }

        public Materia Eliminar(int id)
        {
            var materia = db.Materias.Single(m => m.Id == id);
            db.Materias.Remove(materia);
            return materia;
        }

        public Materia GetMateriaPorId(int Id)
        {
            return this.db.Materias.SingleOrDefault(d => d.Id == Id);
        }

        public IList<Materia> GetMateriasPorNombre(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.ToLower();
            }

            return db.Materias.Where(m => string.IsNullOrEmpty(texto) || m.Nombre.ToLower().Contains(texto)).OrderBy(m => m.Nombre).ToList();
        }

        public object GetTotalMateriasRegistradas()
        {
           return db.Materias.Count();
        }

        public int GuardarCambios()
        {
            return db.SaveChanges();
        }
    }
}
