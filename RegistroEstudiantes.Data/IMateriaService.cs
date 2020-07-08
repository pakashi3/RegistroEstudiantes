using RegistroEstudiantes.Model;
using System.Collections.Generic;
using System.Text;

namespace RegistroEstudiantes.Data
{
    public interface IMateriaService
    {
        IList<Materia> GetMateriasPorNombre(string texto);
        Materia GetMateriaPorId(int Id);

        Materia ActualizarMateria(Materia materiaActualizada);

        Materia CrearMateria(Materia materia);

        int GuardarCambios();
    }
}
