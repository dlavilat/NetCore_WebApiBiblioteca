using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiBiblioteca.Entities.Helpers;

namespace WebApiBiblioteca.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        [Required]
        [StringLength (30, ErrorMessage ="El campo Nombre debe tener {30} caracteres o menos")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Range(18,120)] //La edad debe ser entre 18 y 20
        public int Edad { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [Url]
        public string Url { get; set; }

        public List<Libro> Libros { get; set; }
    }
}
