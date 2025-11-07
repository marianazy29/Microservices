using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ventas.Application.Interfaces;
using Ventas.Application.Response;

namespace Ventas.Infrastructure.ExternalServices
{
    public class ClienteApiClient : IClienteApiClient
    {
        private readonly HttpClient _httpClient;

        public ClienteApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DtoResponseCliente>> ListarClientes()
        {
            var response = await _httpClient.GetAsync("api/Clientes");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DtoResponseCliente>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
       
    }
}
