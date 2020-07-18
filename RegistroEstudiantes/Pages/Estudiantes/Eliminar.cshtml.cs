using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroEstudiantes.Data;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes.Pages.Estudiantes
{
    public class EliminarModel : PageModel
    {
        private readonly IEstudianteService service;
        public Estudiante Estudiante { set; get; }
        public EliminarModel(IEstudianteService service)
        {
            this.service = service;
        }
        public void OnGet(int Id)
        {
            this.Estudiante = service.GetEstudiantesPorId(Id);
        }
        public ActionResult OnPost(int Id)
        {
          var estudiante = service.Eliminar(Id);

          service.GuardarCambios();
          
          TempData["MensajeEliminacion"] = $"Se ha eliminado el Estudiante {estudiante.Nombre}";


            return RedirectToPage("./RegistrodeEstudiantes");

        }
    }
}