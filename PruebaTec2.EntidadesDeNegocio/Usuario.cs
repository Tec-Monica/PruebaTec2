using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTec2.EntidadesDeNegocio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rol")]
        [Required(ErrorMessage = "El Id de rol es requerido")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El largo maximo es de 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es requerido")]
        [MaxLength(50, ErrorMessage = "El largo máximo es de 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Login es requerido")]
        [MaxLength(25, ErrorMessage = "El largo máximo es de  caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password debe estar entre 6 a 50 caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "el estatus es requerido")]
        public byte Estatus { get; set; }

        public Rol Rol { get; set; } //propiedad de navegacion
                                     // tipo de dato | tipo de propiedad


        [NotMapped]
        public int top_aux { get; set; }  //propiedad auxiliar para traer un numero en especifico de registros
                                          // en las consultas

        public enum EnumRol
        {
            Administrador = 1,
            Cliente = 2,
        }
    }

    public enum Estatus_Usuario
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}
