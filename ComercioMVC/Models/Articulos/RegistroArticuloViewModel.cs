namespace ComercioMVC.Models
{
    using System.ComponentModel.DataAnnotations;
    using Dominio.DTOs;

    public class RegistroArticuloViewModel
    {
        [Required(ErrorMessage = "El nombre del artículo es obligatorio.")]
        [MinLength(10, ErrorMessage = "El nombre del artículo debe tener al menos 10 caracteres.")]
        [MaxLength(200, ErrorMessage = "El nombre del artículo no debe exceder los 200 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del artículo es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El código del artículo es obligatorio.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El código del artículo debe tener exactamente 13 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El código del artículo debe contener solo números.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El precio del artículo es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El stock del artículo es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        public RegistroArticuloViewModel() { }

        // Constructor para inicializar desde un DTO
        public RegistroArticuloViewModel(ArticuloDTO articuloDTO)
        {
            Nombre = articuloDTO.Nombre;
            Descripcion = articuloDTO.Descripcion;
            Codigo = articuloDTO.Codigo;
            Precio = articuloDTO.Precio;
            Stock = articuloDTO.Stock;
        }

        // Método para crear un DTO desde el ViewModel
        public ArticuloDTO CrearArticuloDTO()
        {
            return new ArticuloDTO()
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Codigo = Codigo,
                Precio = Precio,
                Stock = Stock
            };
        }
    }


}