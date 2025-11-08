using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Implementations;
using Ventas.Application.Interfaces;
using Ventas.Application.Request;

namespace Ventas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(
            DtoRequestVenta request)
        {
            
           var response= await _ventaService.RegistrarVenta(request);
           return Ok(response);
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _ventaService.ObtenerClientes();
            return Ok(clientes);
        }

    }
}
