using BlazorAdminTemplate.Application.DTOs.Contracts;
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
    public class ContractService : IContractService
    {

        public readonly ApiConfiguration _configuration;
        public readonly HttpClient _httpClient;
        public ITokenService _tokenService;
        private const string MemberContractBeforeSigningEndpoint = "/MemberContractShowBeforeSigning/raw";
        private const string MemberContractSigningEndpoint = "/MemberContractSigning/sign-contract";

        public ContractService(ApiConfiguration configuration, HttpClient httpClient, ITokenService tokenService)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _tokenService = tokenService;
        }



        public async Task<MemberContractBeforeSigningResponseDTO> GetMemberContractBeforeSigningAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_configuration.BaseUrl}{MemberContractBeforeSigningEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var contract = await response.Content.ReadFromJsonAsync<MemberContractBeforeSigningResponseDTO>();
                    return contract ?? new MemberContractBeforeSigningResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberContractBeforeSigningResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching contract: {ex.Message}");
                return new MemberContractBeforeSigningResponseDTO();
            }
        }

        public async Task<MemberContractSigningResponseDTO> PostMemberContractSigningAsync(MemberContractSigningRequestDTO request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync($"{_configuration.BaseUrl}{MemberContractSigningEndpoint}", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberContractSigningResponseDTO>();
                    return result ?? new MemberContractSigningResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberContractSigningResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error signing contract: {ex.Message}");
                return new MemberContractSigningResponseDTO();

            }
        }
    }
}
