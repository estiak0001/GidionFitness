using BlazorAdminTemplate.Application.DTOs.Training;
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
    internal class AccessLogsService : IAccessLogsService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string AccessLogsEndpoint = "/MemberAccessLogsList";
        private const string TotalAccessLogsEndpoint = "/MemberAccessLogsTotal";

        public AccessLogsService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<AccessLogsListReponseDTO> GetAccessLogsAsync(int page)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{AccessLogsEndpoint}?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    var accessLogs = await response.Content.ReadFromJsonAsync<AccessLogsListReponseDTO>();
                    return accessLogs ?? new AccessLogsListReponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new AccessLogsListReponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching access logs: {ex.Message}");
                return new AccessLogsListReponseDTO();
            }
        }

        public async Task<MemberAccessLogsTotalResponseDTO> GetTotalAccessLogsAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{TotalAccessLogsEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var totalAccessLogs = await response.Content.ReadFromJsonAsync<MemberAccessLogsTotalResponseDTO>();
                    return totalAccessLogs ?? new MemberAccessLogsTotalResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberAccessLogsTotalResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total access logs: {ex.Message}");
                return new MemberAccessLogsTotalResponseDTO();
            }
        }
    }
}
