using System.ComponentModel.DataAnnotations;

namespace api.zurich.rarp.Models.Clientes
{
    public class CrearPolizaDto
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public string TipoPoliza { get; set; }
        [Required]
        public DateOnly FechaInicio { get; set; }
        [Required]
        public DateOnly FechaExpiracion { get; set; }
        [Required]
        public decimal MontoAsegurado { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}