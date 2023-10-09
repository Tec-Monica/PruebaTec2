using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTec2.EntidadesDeNegocio
{
    public class Ave
    {
        [Key]

        public int Id { get; set; }

        [ForeignKey("Tipo")]
        [Required(ErrorMessage = "El Id de tipo es requerido")]
        [Display(Name = "Tipo")]
        public int IdTipo { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El largo máximo es de 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La imagen es requerida")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        public Tipo Tipo { get; set; }

        [NotMapped]
        public int top_aux { get; set; }
    }
}
