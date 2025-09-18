using Xunit;
using Moq;
using AutoMapper;
using api.zurich.rarp.Controllers;
using api.zurich.rarp.Models.Clientes;
using api.zurich.rarp.Models.ApiResponse;
using biz.zurich.rarp.Repository.Polizas;
using biz.zurich.rarp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace tests.zurich.rarp.Controllers
{
    public class PolizasControllerTests
    {
        private readonly Mock<IPolizasRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PolizasController _controller;

        public PolizasControllerTests()
        {
            _repoMock = new Mock<IPolizasRepository>();
            _mapperMock = new Mock<IMapper>();
            _controller = new PolizasController(_mapperMock.Object, _repoMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithPolizasList()
        {
            // Arrange
            var polizas = new List<Poliza> { new Poliza { Id = 1, TipoPoliza = "Vida" } };
            _repoMock.Setup(r => r.GetAllIncluding(It.IsAny<System.Linq.Expressions.Expression<System.Func<Poliza, object>>>())).Returns(polizas.AsQueryable());
            _mapperMock.Setup(m => m.Map<List<PolizaDto>>(It.IsAny<List<Poliza>>())).Returns(new List<PolizaDto> { new PolizaDto { Id = 1, TipoPoliza = "Vida" } });

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<List<PolizaDto>>>(okResult.Value);
            Assert.True(response.Success);
            Assert.NotNull(response.Result);
        }
    }
}
