using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Implementations;
using Ventas.Application.Request;

namespace Ventas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly VentaService _ventaService;

        public VentasController(VentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(
            DtoRequestVenta request)
        {
            
            await _ventaService.RegistrarVenta(request);
            return Ok();
        }

    }
}
