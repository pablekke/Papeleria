using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios
{
    public class ServicioArticulo : Servicio<Articulo, ArticuloDTO>, IServicioArticulo
    {
        private readonly IRepositorioArticulo _repositorio;

        public ServicioArticulo(IMapper mapper, IRepositorioArticulo repositorio) : base(mapper, repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<ArticuloDTO> GetArticulosFiltrados(string texto, double monto)
        {
            var articulos = _repositorio.GetArticulosFiltrados(texto, monto);
            foreach (var a in articulos)
            {
                a.Precio = Pedido.TotalMasIVASinRecargo(a.Precio);
                a.Precio = Math.Round(a.Precio, 2);
            }
            return _mapeador.Map<IEnumerable<ArticuloDTO>>(articulos);
        }

        public ArticuloDTO GetArticuloPorId(int id)
        {
            var articulo = _repositorio.GetArticuloPorId(id);
            if(articulo != null)
                articulo.Precio = Pedido.TotalMasIVASinRecargo(articulo.Precio);
            return _mapeador.Map<ArticuloDTO>(articulo);
        }

        #region Excepciones y controles
        protected override void ControlesAntesDeCrear(ArticuloDTO articuloDto)
        {
            if (_repositorio.ExisteCodigo(articuloDto.Codigo))
            {
                throw new Exception("Ya existe un artículo con el mismo código.");
            }
            if (_repositorio.ExisteNombre(articuloDto.Nombre))
            {
                throw new Exception("Ya existe un artículo con el mismo nombre.");
            }
        }
        protected override void ControlesAntesDeActualizar(int id, ArticuloDTO dto)
        {
            var modeloActual = _repositorio.GetPorId(id);
            if (modeloActual == null)
            {
                throw new Exception("Artículo no encontrado.");
            }

            // Verificar cambios en el código y nombre y su existencia en otros artículos
            if (modeloActual.Codigo != dto.Codigo && _repositorio.ExisteCodigo(dto.Codigo))
            {
                throw new Exception("Ya existe un artículo con el nuevo código proporcionado.");
            }

            if (modeloActual.Nombre != dto.Nombre && _repositorio.ExisteNombre(dto.Nombre))
            {
                throw new Exception("Ya existe un artículo con el nuevo nombre proporcionado.");
            }
        }
        #endregion
    }
}