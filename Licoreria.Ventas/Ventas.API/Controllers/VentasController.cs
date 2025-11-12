using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Implementations;
using Ventas.Application.Interfaces;
using Ventas.Application.Request;
using Ventas.Application.UseCases;
using Ventas.Domain.Aggregates;

namespace Ventas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly RegistrarVentaUseCase _registrarVentaUseCase;

        public VentasController(RegistrarVentaUseCase registrarVentaUseCase)
        {
            _registrarVentaUseCase = registrarVentaUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(DtoRequestVenta request)
        {
            var resultado = await _registrarVentaUseCase.EjecutarAsync(request);
            if (resultado == "El cliente no existe.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        /*[HttpGet("clientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _ventaService.ObtenerClientes();
            return Ok(clientes);
        }*/

    }
}
