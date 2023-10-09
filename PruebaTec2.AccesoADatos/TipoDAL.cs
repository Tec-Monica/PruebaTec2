using Microsoft.EntityFrameworkCore;
using PruebaTec2.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTec2.AccesoADatos
{
    public class TipoDAL
    {
        public static async Task<int> CrearAsync(Tipo pTipo)
        {
            int result = 0;

            // Bloque de ejecucion de codigo
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pTipo);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Tipo pTipo)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var tipo = await bdContexto.Tipos.FirstOrDefaultAsync(t => t.Id == pTipo.Id);
                tipo.Nombre = pTipo.Nombre;

                bdContexto.Update(tipo);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Tipo pTipo)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var tipo = await bdContexto.Tipos.FirstOrDefaultAsync(t => t.Id == pTipo.Id);
                bdContexto.Tipos.Remove(tipo);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }


        public static async Task<Tipo> ObtenerPorIdAsync(Tipo pTipo)
        {
            var tipo = new Tipo();
            using (var bdContexto = new BDContexto())
            {
                tipo = await bdContexto.Tipos.FirstOrDefaultAsync(t => t.Id == pTipo.Id);
            }
            return tipo;
        }

        public static async Task<List<Tipo>> ObtenerTodosAsync()
        {
            var tipos = new List<Tipo>();
            using (var bdContexto = new BDContexto())
            {
                tipos = await bdContexto.Tipos.ToListAsync();
            }
            return tipos;
        }

        internal static IQueryable<Tipo> QuerySelect(IQueryable<Tipo> pQuery, Tipo pTipo)
        {
            if (pTipo.Id > 0)
                pQuery = pQuery.Where(t => t.Id == pTipo.Id);


            if (!string.IsNullOrEmpty(pTipo.Nombre))
                pQuery = pQuery.Where(t => t.Nombre.Contains(pTipo.Nombre));

            pQuery = pQuery.OrderByDescending(t => t.Id).AsQueryable();

            if (pTipo.top_aux > 0)
                pQuery = pQuery.Take(pTipo.top_aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Tipo>> BuscarAsync(Tipo pTipo)
        {
            var tipos = new List<Tipo>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Tipos.AsQueryable();
                select = QuerySelect(select, pTipo);
                tipos = await select.ToListAsync();
            }
            return tipos;
        }
    }
}
