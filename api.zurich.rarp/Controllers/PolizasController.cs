using api.zurich.rarp.Models.ApiResponse;
using AutoMapper;
using biz.zurich.rarp.Repository.Polizas;
using Microsoft.AspNetCore.Mvc;
using api.zurich.rarp.Models.Clientes;
using biz.zurich.rarp.Entities;

namespace api.zurich.rarp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolizasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPolizasRepository _polizasRepository;

        public PolizasController(IMapper mapper, IPolizasRepository polizasRepository)
        {
            _mapper = mapper;
            _polizasRepository = polizasRepository;
        }

        [HttpGet("ObtenerTodasPolizas", Name = "ObtenerTodasPolizas")]
        public ActionResult<ApiResponse<List<PolizaDto>>> GetAll()
        {
            var response = new ApiResponse<List<PolizaDto>>();
            try
            {
                var polizas = _polizasRepository.GetAllIncluding(x => x.Cliente).ToList();
                response.Result = _mapper.Map<List<PolizaDto>>(polizas);
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

        [HttpGet("ObtenerPolizaPorId", Name = "ObtenerPolizaPorId")]
        public ActionResult<ApiResponse<List<PolizaDto>>> GetById(int id)
        {
            var response = new ApiResponse<List<PolizaDto>>();
            try
            {
                var polizas = _polizasRepository.FindBy(x => x.Id == id).ToList();
                response.Result = _mapper.Map<List<PolizaDto>>(polizas);
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

        [HttpGet("ObtenerPolizaPorEmail", Name = "ObtenerPolizaPorEmail")]
        public ActionResult<ApiResponse<List<PolizaDto>>> GetByEmail(int clienteId)
        {
            var response = new ApiResponse<List<PolizaDto>>();
            try
            {
                var polizas = _polizasRepository.FindBy(x => x.ClienteId == clienteId).ToList();
                response.Result = _mapper.Map<List<PolizaDto>>(polizas);
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

        [HttpPost("CrearPoliza", Name = "CrearPoliza")]
        public ActionResult<ApiResponse<CrearPolizaDto>> CreatePoliza(CrearPolizaDto item)
        {
            var response = new ApiResponse<CrearPolizaDto>();
            try
            {
                // Validación de fechas para cumplir con el CHECK constraint
                if (item.FechaExpiracion < item.FechaInicio)
                {
                    response.Success = false;
                    response.Message = "La fecha de expiración debe ser mayor o igual a la fecha de inicio.";
                    response.Result = null;
                    return BadRequest(response);
                }
                var poliza = _mapper.Map<Poliza>(item);
                var polizaCreada = _polizasRepository.Add(poliza);
                response.Result = _mapper.Map<CrearPolizaDto>(polizaCreada);
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

        [HttpPut("ActualizarPoliza", Name = "ActualizarPoliza")]
        public ActionResult<ApiResponse<UpdatePolizaDto>> UpdatePoliza(UpdatePolizaDto item)
        {
            var response = new ApiResponse<UpdatePolizaDto>();
            try
            {
                // Validación de fechas para cumplir con el CHECK constraint
                if (item.FechaExpiracion < item.FechaInicio)
                {
                    response.Success = false;
                    response.Message = "La fecha de expiración debe ser mayor o igual a la fecha de inicio.";
                    response.Result = null;
                    return BadRequest(response);
                }
                var poliza = _mapper.Map<Poliza>(item);
                var polizaActualizada = _polizasRepository.Update(poliza, item.Id);
                response.Result = _mapper.Map<UpdatePolizaDto>(polizaActualizada);
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

        [HttpDelete("EliminarPoliza", Name = "EliminarPoliza")]
        public ActionResult<ApiResponse<PolizaDto>> DeletePoliza(PolizaDto item)
        {
            var response = new ApiResponse<PolizaDto>();
            try
            {
                var poliza = _polizasRepository.Find(x => x.Id == item.Id);
                _polizasRepository.Delete(poliza);
                response.Result = item;
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
