using Xunit;
using dal.zurich.rarp.Repository.Polizas;
using dal.zurich.rarp.DBContext;
using Microsoft.EntityFrameworkCore;
using biz.zurich.rarp.Entities;
using System;
using System.Linq;

namespace tests.zurich.rarp.Repositories
{
    public class PolizasRepositoryTests
    {
        [Fact]
        public void Add_And_Get_Poliza_Works()
        {
            var options = new DbContextOptionsBuilder<ZurichRarpContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2").Options;
            using var context = new ZurichRarpContext(options);
            var usuario = new Usuario { NumeroIdentificacion = "1234567890", NombreCompleto = "Test", CorreoElectronico = "test@test.com", Telefono = "123456789", Direccion = "Calle 1", RolId = 1 };
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            var repo = new PolizasRepository(context);
            var poliza = new Poliza { ClienteId = usuario.Id, TipoPoliza = "Vida", FechaInicio = DateOnly.FromDateTime(DateTime.Now), FechaExpiracion = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), MontoAsegurado = 1000, Estado = "Activa" };
            repo.Add(poliza);
            var result = repo.GetAll().FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal("Vida", result.TipoPoliza);
        }
    }
}
