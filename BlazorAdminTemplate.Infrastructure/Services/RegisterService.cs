using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class RegisterService : IRegisterService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private readonly string _registerEndpoint = "/Auth/register";
        private readonly string StaffRegisterEndpoint = "/Auth/registerstaff";

        public RegisterService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            try 
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{_registerEndpoint}", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<RegisterResponseDTO>();
                    RegistrationCompleted?.Invoke(true);
                    return result;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorMessage);
                    RegistrationCompleted?.Invoke(false);
                    return new RegisterResponseDTO();
                }
            }
            catch (Exception ex)
            {
                RegistrationCompleted?.Invoke(false);
                Console.WriteLine($"Registration failed: {ex.Message}");
                return new RegisterResponseDTO();
            }
        }

        public async Task<RegisterResponseDTO> StaffRegisterAsync(RegisterRequestDTO request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffRegisterEndpoint}", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<RegisterResponseDTO>();
                    RegistrationCompleted?.Invoke(true);
                    return result ?? new RegisterResponseDTO { ErrorMessage = "Tom respons fra server" };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorMessage}");
                    RegistrationCompleted?.Invoke(false);
                    return new RegisterResponseDTO { ErrorMessage = $"API fejl: {response.StatusCode} - {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                RegistrationCompleted?.Invoke(false);
                Console.WriteLine($"Staff registration failed: {ex.Message}");
                return new RegisterResponseDTO { ErrorMessage = ex.Message };
            }
        }

        public event Action<bool>? RegistrationCompleted;

        
    }
}
