using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEstudiantes.Pages
{
    public class ConteoEstudiantesViewComponent: ViewComponent
    {
        private readonly IEstudianteService service;

        public ConteoEstudiantesViewComponent(IEstudianteService service)
        {
            this.service = service;
        }

        public IViewComponentResult Invoke()
        {
            var totalEstudiantes = service.GetTotalEstudaintesRegistrados();
            return View("ConteoEstudiantes", totalEstudiantes);
        }
    }
}
