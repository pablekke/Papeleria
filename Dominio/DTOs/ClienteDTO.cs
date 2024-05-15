namespace Dominio.DTOs
{
    public class ClienteDTO
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string RazonSocial { get; set; }
        public string RUT { get; set; }
        public DireccionDTO Direccion { get; set; }
        public double Distancia { get; set; }
    }
}
