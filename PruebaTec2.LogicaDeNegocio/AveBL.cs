using PruebaTec2.AccesoADatos;
using PruebaTec2.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTec2.LogicaDeNegocio
{
    public class AveBL
    {
        public async Task<int> CrearAsync(Ave pAve)
        {
            return await AveDAL.CrearAsync(pAve);
        }
        public async Task<int> ModificarAsync(Ave pAve)
        {
            return await AveDAL.ModificarAsync(pAve);
        }
        public async Task<int> EliminarAsync(Ave pAve)
        {
            return await AveDAL.EliminarAsync(pAve);
        }
        public async Task<Ave> ObtenerPorIdAsync(Ave pAve)
        {
            return await AveDAL.ObtenerPorIdAsync(pAve);
        }
        public async Task<List<Ave>> ObtenerTodosAsync()
        {
            return await AveDAL.ObtenerTodosAsync();
        }
        public async Task<List<Ave>> BuscarAsync(Ave pAve)
        {
            return await AveDAL.BuscarAsync(pAve);
        }
    }
}
