using Xunit;
using dal.zurich.rarp.Repository.Clientes;
using dal.zurich.rarp.DBContext;
using Microsoft.EntityFrameworkCore;
using biz.zurich.rarp.Entities;
using System.Linq;

namespace tests.zurich.rarp.Repositories
{
    public class ClientesRepositoryTests
    {
        [Fact]
        public void Add_And_Get_Cliente_Works()
        {
            var options = new DbContextOptionsBuilder<ZurichRarpContext>()
                .UseInMemoryDatabase(databaseName: "TestDb1").Options;
            using var context = new ZurichRarpContext(options);
            var repo = new ClientesRepository(context);
            var usuario = new Usuario { NumeroIdentificacion = "1234567890", NombreCompleto = "Test", CorreoElectronico = "test@test.com", Telefono = "123456789", Direccion = "Calle 1", RolId = 1 };
            repo.Add(usuario);
            var result = repo.GetAll().FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal("1234567890", result.NumeroIdentificacion);
        }
    }
}
