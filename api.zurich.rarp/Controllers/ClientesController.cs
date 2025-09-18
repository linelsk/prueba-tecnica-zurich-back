using api.zurich.rarp.Models.ApiResponse;
using AutoMapper;
using biz.zurich.rarp.Repository.Clientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.zurich.rarp.Models.Clientes;
using biz.zurich.rarp.Entities;

namespace api.zurich.rarp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IClientesRepository _clientesRepository;

        public ClientesController(IMapper mapper, IClientesRepository clientesRepository)
        {
            _mapper = mapper;
            _clientesRepository = clientesRepository;
        }

        [HttpGet("ObtenerTodosClientes", Name = "ObtenerTodosClientes")]
        public ActionResult<ApiResponse<List<ClientesDto>>> GetAll()
        {
            var response = new ApiResponse<List<ClientesDto>>();

            try
            {
                var clientes = _clientesRepository.GetAllIncluding(x => x.Polizas).ToList();
                response.Result = _mapper.Map<List<ClientesDto>>(clientes);
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("ObtenerClientePorId", Name = "ObtenerClientePorId")]
        public ActionResult<ApiResponse<List<ClientesDto>>> GetById(int id)
        {
            var response = new ApiResponse<List<ClientesDto>>();

            try
            {
                var clientes = _clientesRepository.FindBy(x => x.Id == id).ToList();
                response.Result = _mapper.Map<List<ClientesDto>>(clientes);
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("ObtenerClientePorNumeroDeIdentificacion", Name = "ObtenerClientePorNumeroDeIdentificacion")]
        public ActionResult<ApiResponse<List<ClientesDto>>> GetByNoIdenti(string numero, string contrasena)
        {
            var response = new ApiResponse<List<ClientesDto>>();

            try
            {
                var clientes = _clientesRepository.FindBy(x => x.NumeroIdentificacion == numero && x.Contrasena == contrasena).ToList();
                response.Result = _mapper.Map<List<ClientesDto>>(clientes);
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("CrearCliente", Name = "CrearCliente")]
        public ActionResult<ApiResponse<CrearClientesDto>> CreateClient(CrearClientesDto item)
        {
            var response = new ApiResponse<CrearClientesDto>();

            try
            {
                if (_clientesRepository.Exists(x => x.NumeroIdentificacion == item.NumeroIdentificacion))
                {
                    response.Success = false;
                    response.Message = "Ya existe un cliente con el mismo número de identificación.";
                    response.Result = null;
                    return StatusCode(201, response);
                }

                var cliente = _mapper.Map<Usuario>(item);
                cliente.RolId = 2; // O el valor correspondiente
                cliente.Contrasena = "cliente";
                var clienteCreado = _clientesRepository.Add(cliente);
                response.Message = "Cliente creado correctamente";
                response.Result = _mapper.Map<CrearClientesDto>(clienteCreado);
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }

        [HttpPut("ActualizarCliente", Name = "ActualizarCliente")]
        public ActionResult<ApiResponse<UpdateClientesDto>> UpdateClient(UpdateClientesDto item)
        {
            var response = new ApiResponse<UpdateClientesDto>();

            try
            {

                var cliente = _mapper.Map<Usuario>(item);
                cliente.RolId = 2; // O el valor correspondiente
                cliente.Contrasena = "cliente";
                var clienteActualizado = _clientesRepository.Update(cliente, item.Id);
                response.Result = _mapper.Map<UpdateClientesDto>(clienteActualizado);

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }

        [HttpDelete("EliminarCliente", Name = "EliminarCliente")]
        public ActionResult<ApiResponse<UpdateClientesDto>> DeleteClient(int id)
        {
            var response = new ApiResponse<UpdateClientesDto>();

            try
            {
                var cliente = _clientesRepository.Find(x => x.Id == id);
                _clientesRepository.Delete(cliente);
                response.Result = null;
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }
    }
}
