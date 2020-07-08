using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroEstudiantes.Data;
using RegistroEstudiantes.Model;

namespace RegistroEstudiantes.Pages.Materias
{
    public class EditarModel : PageModel
    {
        private readonly IMateriaService service;
        private readonly IHtmlHelper helper;

        [BindProperty]
        public Materia Materia { get; set; }

        public IEnumerable<SelectListItem> Areas { get; set; }

        public EditarModel(IMateriaService service, IHtmlHelper helper)
        {
            this.service = service;
            this.helper = helper;
        }

        public void OnGet(int? Id)
        {
            Areas = helper.GetEnumSelectList<Area>();

            if (Id.HasValue)
            {
                Materia = service.GetMateriaPorId(Id.Value);
            }
            else 
            {
                Materia = new Materia();
            }
        }

        public ActionResult OnPost() 
        {
            Areas = helper.GetEnumSelectList<Area>();
            
            if (ModelState.IsValid) 
            {
                if (Materia.Id == 0)
                {
                    Materia = service.CrearMateria(Materia);
                    TempData["Mensaje"] = "Registro Creado Correctamente";
                }
                else 
                {
                    Materia = service.ActualizarMateria(Materia);
                    TempData["Mensaje"] = "Registro Actualizado Correctamente";
                }
                service.GuardarCambios();

                return RedirectToPage("./DetalleMateria", new { Id = Materia.Id });
            }

            return Page();
        }
    }
}
