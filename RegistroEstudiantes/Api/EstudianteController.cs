using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.Data;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private IEstudianteService service;

        public EstudianteController(IEstudianteService service)
        {
            this.service = service;
        }
        [Route("Hola Mundo")]
        public string HolaMundo()
        {
            return "Hola Desde Materia Controller";
        }

        [Route("Estudiantes")]
        public List<Estudiante> GetMaterias()
        {
            var estudiantes = service.GetEstudiantesPorNombre("");
            return estudiantes.ToList();
        }
    }
}
