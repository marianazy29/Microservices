using Clientes.Application.Implementations;
using Clientes.Application.Request;
using Clientes.Application.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Clientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DtoResponseUsuario?>> GetCliente(Guid id)
        {
            var cliente = await _clienteService.GetCliente(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(DtoRequestCliente request)
        {
            await _clienteService.CrearCliente(request);
            return Created();
        }

        [HttpPost("{clienteId}/sumar-puntos")]
        public async Task<IActionResult> SumarPuntos(Guid clienteId, double puntos)
        {
            await _clienteService.SumarPuntos(clienteId, puntos);
            return NoContent();
        }

        [HttpPost("{clienteId}/entregar-premio")]
        public async Task<IActionResult> EntregarPremio(Guid clienteId, string descripcion, Guid prodcutoId)
        {
            await _clienteService.EntregarPremio(clienteId, descripcion, prodcutoId);
            return NoContent();
        }

    }
}
