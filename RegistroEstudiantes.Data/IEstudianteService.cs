using System.Collections.Generic;
using System.Text;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes.Data
{
    public interface IEstudianteService
    {
        IList<Estudiante> GetEstudiantesPorNombre( string texto);
        Estudiante GetEstudiantesPorId(int Id);

        Estudiante ActualizarEstudiante(Estudiante estudianteActualizado);

        Estudiante CrearEstudiante(Estudiante estudiante);

        int GuardarCambios();
    }
}

