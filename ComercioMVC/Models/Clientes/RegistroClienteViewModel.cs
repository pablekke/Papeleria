using System.ComponentModel.DataAnnotations;
using Dominio.DTOs;

public class RegistroClienteViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe proporcionar una dirección de email válida.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La razón social es obligatoria.")]
    public string RazonSocial { get; set; }

    [Required(ErrorMessage = "El RUT es obligatorio.")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El RUT solo puede contener números.")]
    [StringLength(12, ErrorMessage = "El RUT no debe exceder los 12 caracteres.")]
    public string RUT { get; set; }

    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    public string Ciudad { get; set; }

    [Required(ErrorMessage = "La calle es obligatoria.")]
    public string Calle { get; set; }

    [Required(ErrorMessage = "El número de puerta es obligatorio.")]
    public int NumeroPuerta { get; set; }

    public double Distancia { get; set; }

    public RegistroClienteViewModel() { }

    public RegistroClienteViewModel(ClienteDTO clienteDTO)
    {
        Nombre = clienteDTO.Nombre;
        Apellido = clienteDTO.Apellido;
        Email = clienteDTO.Email;
        RazonSocial = clienteDTO.RazonSocial;
        RUT = clienteDTO.RUT;
        Ciudad = clienteDTO.Direccion.Ciudad;
        Calle = clienteDTO.Direccion.Calle;
        NumeroPuerta = clienteDTO.Direccion.NumeroPuerta;
        Distancia = clienteDTO.Distancia;
    }

    public ClienteDTO CrearClienteDTO()
    {
        return new ClienteDTO()
        {
            Nombre = Nombre,
            Apellido = Apellido,
            Email = Email,
            RazonSocial = RazonSocial,
            RUT = RUT,
            Direccion = new DireccionDTO
            {
                Ciudad = Ciudad,
                Calle = Calle,
                NumeroPuerta = NumeroPuerta
            },
            Distancia = Distancia
        };
    }
}
