using BlazorAdminTemplate.Application.DTOs.ContractsDTOs;
using BlazorAdminTemplate.Application.DTOs.Training;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    internal class MemberContractService : IMemberContractService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberContractEndpoint = "/MemberContractShowMySignedContracts";
        private const string TotalMemeberContractEndpoint = "/MemberContractShowMySignedContractsTotal";


        public MemberContractService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<MemberContractListResponseDTO> GetMemberContractAsync(int page)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberContractEndpoint}?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    var accessLogs = await response.Content.ReadFromJsonAsync<MemberContractListResponseDTO>();
                    return accessLogs ?? new MemberContractListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberContractListResponseDTO();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching access logs: {ex.Message}");
                return new MemberContractListResponseDTO();
            }
        }

        public async Task<MemberContractTotalResponseDTO> GetTotalMemberContractsAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{TotalMemeberContractEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var totalAccessLogs = await response.Content.ReadFromJsonAsync<MemberContractTotalResponseDTO>();
                    return totalAccessLogs ?? new MemberContractTotalResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberContractTotalResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total access logs: {ex.Message}");
                return new MemberContractTotalResponseDTO();
            }
        }
    }
}
