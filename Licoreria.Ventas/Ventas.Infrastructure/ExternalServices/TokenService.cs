using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Interfaces;
using Ventas.Application.Response;

namespace Ventas.Infrastructure.ExternalServices
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TokenService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> ObtenerTokenDeAcceso()
        {
            var request = new Dictionary<string, string>
            {
                { "client_id", _configuration["Keycloak:ClientId"] },
                { "client_secret", _configuration["Keycloak:ClientSecret"] },
                { "grant_type", "client_credentials" }
            };

            var response = await _httpClient.PostAsync(
            $"{_configuration["Keycloak:Authority"]}/protocol/openid-connect/token",
            new FormUrlEncodedContent(request));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<DtoTokenResponse>();

            return content?.access_token ?? throw new Exception("Token no recibido");
        }
    }
}
