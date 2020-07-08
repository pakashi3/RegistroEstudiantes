using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroEstudiantes.Data;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes
{
    public class DetalleMateriaModel : PageModel
    {
        private readonly IMateriaService service;
        public Materia Materia { get; set; }

        [TempData]
        public string Mensaje { get; set; }

        public DetalleMateriaModel(IMateriaService service)
        {
            this.service = service;
        }

        public IActionResult OnGet(int Id)
        {
            Materia = service.GetMateriaPorId(Id);

            if (Materia == null)
            {
                return RedirectToPage("MateriaNoEncontrada");
            }
            else 
            {
                return Page();
            }
        }
    }
}