using PruebaTec2.AccesoADatos;
using PruebaTec2.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTec2.LogicaDeNegocio
{
    public class TipoBL
    {
        public async Task<int> CrearAsync(Tipo pTipo)
        {
            return await TipoDAL.CrearAsync(pTipo);
        }

        public async Task<int> ModificarAsync(Tipo pTipo)
        {
            return await TipoDAL.ModificarAsync(pTipo);
        }

        public async Task<int> EliminarAsync(Tipo pTipo)
        {
            return await TipoDAL.EliminarAsync(pTipo);
        }

        public async Task<Tipo> ObtenerPorIdAsync(Tipo pTipo)
        {
            return await TipoDAL.ObtenerPorIdAsync(pTipo);
        }

        public async Task<List<Tipo>> ObtenerTodosAsync()
        {
            return await TipoDAL.ObtenerTodosAsync();
        }

        public async Task<List<Tipo>> BuscarAsync(Tipo pTipo)
        {
            return await TipoDAL.BuscarAsync(pTipo);
        }
    }
}
