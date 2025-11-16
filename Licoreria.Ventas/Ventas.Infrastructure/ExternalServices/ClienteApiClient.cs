using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        private readonly ITokenService _tokenService;

        public ClienteApiClient(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<bool> GetByid(Guid id)
        {
            var token = await _tokenService.ObtenerTokenDeAcceso();

            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/Clientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                
                return true;
            }

            return false;
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
