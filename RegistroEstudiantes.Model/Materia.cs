using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroEstudiantes.Model
{
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es requerido"), MinLength(5, ErrorMessage = "Nombre muy corto. Debe contener almenos 5 caracteres"), MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(4)]
        public string Codigo { get; set; }

        [Required]
        public Area Area { get; set; }

        public bool Disponible { get; set; }

        [Required, MinLength(10), MaxLength(1000)]
        public string Objetivos { get; set; }
    }

}
