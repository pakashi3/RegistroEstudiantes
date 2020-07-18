using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEstudiantes.Pages
{
    public class ConteoMateriasViewComponent : ViewComponent
    {
        private IMateriaService service;

        public ConteoMateriasViewComponent(IMateriaService service)
        {
            this.service = service;
        }

        public IViewComponentResult Invoke()
        {
            var totalMaterias = service.GetTotalMateriasRegistradas();

            return View("ConteoMaterias",totalMaterias); 
        }
    }
}
