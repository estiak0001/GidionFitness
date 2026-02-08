using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.DTOs.Payment;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using Flurl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class PaymentGroupService : IPaymentGroupService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private readonly string _paymentGroupEndpoint = "/MemberPaymentGroup/by-organisation";
        private readonly string _staffPaymentGroupEndpoint = "/StaffMemberPaymentGroup";
        private readonly string _paymentProvidersEndpoint = "/StaffListPaymentProviderData/ListPaymentProviders";
        private readonly string _defaultGroupsEndpoint = "/StaffListMemberGroupDefaultGroups/ListMemberGroupDefaultGroups";

        public PaymentGroupService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<PaymentGroupResponseDTO> GetPaymentGroupAsync(string orgMainGuid, string orgSubGuid)
        {
            try 
            {
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{_paymentGroupEndpoint}?orgMainGuid={orgMainGuid}&orgSubGuid={orgSubGuid}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PaymentGroupResponseDTO>()
                        ?? new PaymentGroupResponseDTO();
                }
                else
                {
                    throw new Exception($"Failed to retrieve payment group: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving payment group: {ex.Message}");
                return new PaymentGroupResponseDTO();
            }
        }

        public async Task<StaffPaymentGroupListResponseDTO> GetPaymentGroupsAsync(int page = 1, int pageSize = 15, string search = "")
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"{_apiConfiguration.BaseUrl}{_staffPaymentGroupEndpoint}"
                .SetQueryParams(new { page, pageSize, search });

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentGroupListResponseDTO>()
                    ?? new StaffPaymentGroupListResponseDTO();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to retrieve payment groups: {errorMessage}");
            }
        }

        public async Task<StaffPaymentGroupSingleResponseDTO> GetPaymentGroupByIdAsync(string groupGuid)
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{_staffPaymentGroupEndpoint}/{groupGuid}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentGroupSingleResponseDTO>()
                    ?? new StaffPaymentGroupSingleResponseDTO();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to retrieve payment group: {errorMessage}");
            }
        }

        public async Task<StaffPaymentGroupResponseDTO> CreatePaymentGroupAsync(StaffPaymentGroupCreateDTO request)
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{_staffPaymentGroupEndpoint}/create", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentGroupResponseDTO>()
                    ?? new StaffPaymentGroupResponseDTO { Message = "Payment group created successfully." };
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create payment group: {errorMessage}");
            }
        }

        public async Task<StaffPaymentGroupResponseDTO> UpdatePaymentGroupAsync(StaffPaymentGroupUpdateDTO request)
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{_staffPaymentGroupEndpoint}/edit", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentGroupResponseDTO>()
                    ?? new StaffPaymentGroupResponseDTO { Message = "Payment group updated successfully." };
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to update payment group: {errorMessage}");
            }
        }

        public async Task<StaffPaymentGroupResponseDTO> DeletePaymentGroupAsync(string groupGuid)
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{_staffPaymentGroupEndpoint}/delete/{groupGuid}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentGroupResponseDTO>()
                    ?? new StaffPaymentGroupResponseDTO { Message = "Payment group deleted successfully." };
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete payment group: {errorMessage}");
            }
        }

        public async Task<PaymentProviderListResponseDTO> GetPaymentProvidersAsync()
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{_paymentProvidersEndpoint}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PaymentProviderListResponseDTO>()
                    ?? new PaymentProviderListResponseDTO();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to retrieve payment providers: {errorMessage}");
            }
        }

        public async Task<MemberGroupDefaultListResponseDTO> GetDefaultGroupsAsync()
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{_defaultGroupsEndpoint}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MemberGroupDefaultListResponseDTO>()
                    ?? new MemberGroupDefaultListResponseDTO();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to retrieve default groups: {errorMessage}");
            }
        }
    }
}
