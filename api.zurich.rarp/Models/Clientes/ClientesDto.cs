using biz.zurich.rarp.Entities;
using System.ComponentModel.DataAnnotations;

namespace api.zurich.rarp.Models.Clientes
{
    public class ClientesDto
    {
        public int Id { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string NombreCompleto { get; set; }

        public string CorreoElectronico { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public int RolId { get; set; }

        public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();

        public virtual Role Rol { get; set; }
    }

    public class CrearClientesDto
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El número de identificacion es obligatorio")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El Número de identificacion debe contener 10 caracteres")]
        public string NumeroIdentificacion { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La dirección es obligatorio")]
        public string Direccion { get; set; }

    }

    public class UpdateClientesDto
    {
        [Required(ErrorMessage = "El ID es obligatorio")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de identificacion es obligatorio")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El Número de identificacion debe contener 10 caracteres")]
        public string NumeroIdentificacion { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La dirección es obligatorio")]
        public string Direccion { get; set; }

    }
}
