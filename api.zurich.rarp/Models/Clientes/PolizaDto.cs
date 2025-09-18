using System.ComponentModel.DataAnnotations;

namespace api.zurich.rarp.Models.Clientes
{
    public class PolizaDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string TipoPoliza { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaExpiracion { get; set; }
        public decimal MontoAsegurado { get; set; }
        public string Estado { get; set; }
    }
}