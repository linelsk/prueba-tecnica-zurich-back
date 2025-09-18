using Xunit;
using Moq;
using AutoMapper;
using api.zurich.rarp.Controllers;
using api.zurich.rarp.Models.Clientes;
using api.zurich.rarp.Models.ApiResponse;
using biz.zurich.rarp.Repository.Clientes;
using biz.zurich.rarp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace tests.zurich.rarp.Controllers
{
    public class ClientesControllerTests
    {
        private readonly Mock<IClientesRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ClientesController _controller;

        public ClientesControllerTests()
        {
            _repoMock = new Mock<IClientesRepository>();
            _mapperMock = new Mock<IMapper>();
            _controller = new ClientesController(_mapperMock.Object, _repoMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithClientesList()
        {
            // Arrange
            var clientes = new List<Usuario> { new Usuario { Id = 1, NombreCompleto = "Test" } };
            _repoMock.Setup(r => r.GetAllIncluding(It.IsAny<System.Linq.Expressions.Expression<System.Func<Usuario, object>>>())).Returns(clientes.AsQueryable());
            _mapperMock.Setup(m => m.Map<List<ClientesDto>>(It.IsAny<List<Usuario>>())).Returns(new List<ClientesDto> { new ClientesDto { Id = 1, NombreCompleto = "Test" } });

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = Assert.IsType<ApiResponse<List<ClientesDto>>>(okResult.Value);
            Assert.True(response.Success);
            Assert.NotNull(response.Result);
        }
    }
}
