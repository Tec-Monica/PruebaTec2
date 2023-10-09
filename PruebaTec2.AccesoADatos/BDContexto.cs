using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTec2.EntidadesDeNegocio;


namespace PruebaTec2.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Ave> Aves { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=MONICA;Initial Catalog=PruebaTec2;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
