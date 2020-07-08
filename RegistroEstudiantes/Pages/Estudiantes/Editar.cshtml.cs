using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroEstudiantes.Data;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes.Pages.Estudiantes
{
    public class EditarModel : PageModel
    {
        private readonly IEstudianteService service;
        private readonly IHtmlHelper helper;

        [BindProperty]
        public Estudiante Estudiante { get; set; }

        public IEnumerable<SelectListItem> Carreras { get; set; }
        public IEnumerable<SelectListItem> Sexos { get; set; }

        public EditarModel(IEstudianteService service, IHtmlHelper helper)
        {
            this.service = service;
            this.helper = helper;
        }
        public void OnGet(int? Id)
        {
            Carreras = helper.GetEnumSelectList<Carrera>();
            Sexos = helper.GetEnumSelectList<Sexo>();

            if (Id.HasValue)
            {
                Estudiante = service.GetEstudiantesPorId(Id.Value);
            }
            else
            {
                Estudiante = new Estudiante();
            }
        }
        public ActionResult OnPost( )
        {
            //Carreras = helper.GetEnumSelectList<Carrera>();

            if (ModelState.IsValid)
            {
                if (Estudiante.Id == 0)
                {
                    Estudiante = service.CrearEstudiante(Estudiante);
                    TempData["Mensaje"] = "Registro Creado Correctamente";
                }
                else
                {
                    Estudiante = service.ActualizarEstudiante(Estudiante);
                    TempData["Mensaje"] = "Registro Actualizado Correctamente";
                }

                service.GuardarCambios();
                return RedirectToPage("DetalleEstudiante", new { Id = Estudiante.Id });
            }

            return Page();
            
           
        }
    }
}