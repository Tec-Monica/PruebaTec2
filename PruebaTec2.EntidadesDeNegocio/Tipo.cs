﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTec2.EntidadesDeNegocio
{
    public class Tipo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(30, ErrorMessage = "El largo máximo son de 30 caracteres")]
        public string Nombre { get; set; }
        public List<Ave> Aves { get; set; } 


        [NotMapped]
        public int top_aux { get; set; } //propíedad auxiliar para seleccionar cantidad de datos a consultar 
    }
}
