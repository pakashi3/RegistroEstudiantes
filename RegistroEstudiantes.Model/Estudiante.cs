using System;
using System.Collections.Generic;
using System.Text;
using RegistroEstudiantes.Model;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RegistroEstudiantes.Model
{
    public class Estudiante
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        public string Matricula { get; set; }

        //[Required(ErrorMessage = "Nombre es requerido"), MinLength(5, ErrorMessage = "Nombre muy corto. Debe contener almenos 5 caracteres"), MaxLength(255)]
        [Required(ErrorMessage ="El nombre es requerido")]
        [MinLength(5, ErrorMessage ="Nombre muy corto, debe contener almenos 5 caracteres")]
        [MaxLength(255)]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = "El apellido es requerido")]
        [MinLength(5)]
        [MaxLength(255)]
        public string Apellido { get; set; }

        //[Required(ErrorMessage = "La Edad es requerida")]
        // [MinLength(10)]


        [Required(ErrorMessage = "La fecha es requerido")]
        [DataType(DataType.Date)]
        public DateTime FechaNac { get; set; }

        public int Edad { get; set; }

        [Required(ErrorMessage = "Descripcion de tu meta en la vida es requerido")]
        [MinLength(10)]
        [MaxLength(1000)]
        public String Meta { get; set; }

        [Required(ErrorMessage = "No puede ingresar un estudiante sin especificar la carrera")]
        public Carrera Carrera { get; set; }
        
        [Required]
        public Sexo Sexo { get; set; }

        //public int Max(Func<object, object> p)
        //{
            //throw new NotImplementedException();
        //}

        //public void Add(Estudiante estudiante)
        //{
            //throw new NotImplementedException();
        //}

        //[Required, MinLength(10), MaxLength(1000)]
        //public string  { get; set; }
    }
}
