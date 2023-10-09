using Microsoft.EntityFrameworkCore;
using PruebaTec2.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTec2.AccesoADatos
{
    public  class AveDAL
    {
        public static async Task<int> CrearAsync(Ave pAve)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pAve);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Ave pAve)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var ave = await bdContexto.Aves.FirstOrDefaultAsync(a => a.Id == pAve.Id);
                ave.Nombre = pAve.Nombre;
                ave.Imagen = pAve.Imagen;
                ave.Descripcion = pAve.Descripcion;

                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Ave pAve)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var ave = await bdContexto.Aves.FirstOrDefaultAsync(a => a.Id == pAve.Id);
                bdContexto.Aves.Remove(ave);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Ave> ObtenerPorIdAsync(Ave pAve)
        {
            var ave = new Ave();
            using (var bdContexto = new BDContexto())
            {
                ave = await bdContexto.Aves.FirstOrDefaultAsync(a => a.Id == pAve.Id);
            }
            return ave;
        }

        public static async Task<List<Ave>> ObtenerTodosAsync()
        {
            var aves = new List<Ave>();
            using (var bdContexto = new BDContexto())
            {
                aves = await bdContexto.Aves.ToListAsync();
            }
            return aves;
        }
        internal static IQueryable<Ave> QuerySelect(IQueryable<Ave> pQuery,
                                             Ave pAve)
        {
            if (pAve.Id > 0)
                pQuery = pQuery.Where(e => e.Id == pAve.Id);

            if (pAve.IdTipo > 0)
                pQuery = pQuery.Where(u => u.IdTipo == pAve.IdTipo);

            if (!string.IsNullOrEmpty(pAve.Nombre))
                pQuery = pQuery.Where(a => a.Nombre.Contains(pAve.Nombre));

            if (!string.IsNullOrEmpty(pAve.Descripcion))
                pQuery = pQuery.Where(a => a.Descripcion.Contains(pAve.Descripcion));

            pQuery = pQuery.OrderByDescending(a => a.Id).AsQueryable();
            if (pAve.top_aux > 0)
                pQuery = pQuery.Take(pAve.top_aux).AsQueryable();

            return pQuery;
        }
        public static async Task<List<Ave>> BuscarAsync(Ave pAve)
        {
            var aves = new List<Ave>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Aves.AsQueryable();
                select = QuerySelect(select, pAve);
                aves = await select.ToListAsync();
            }
            return aves;
        }
    }
}
