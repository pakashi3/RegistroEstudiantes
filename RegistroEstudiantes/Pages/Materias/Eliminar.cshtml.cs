using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroEstudiantes.Data;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes.Pages.Materias
{
    public class EliminarModel : PageModel
    {
        private readonly IMateriaService service;
        public Materia Materia { get; set; }
        public EliminarModel(IMateriaService service)
        {
            this.service = service;
        }

        public void OnGet(int Id)
        {
            this.Materia = service.GetMateriaPorId(Id);
        }

        public ActionResult OnPost (int Id)
        {
           var materia =  service.Eliminar(Id);

           service.GuardarCambios();

            TempData["MensajeEliminacion"] = $"Se ha Eliminado la materia {materia.Nombre}";



            return RedirectToPage("./RegistroMaterias");
        }
    }
}