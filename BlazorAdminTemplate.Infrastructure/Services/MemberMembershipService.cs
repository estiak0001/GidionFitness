using BlazorAdminTemplate.Application.DTOs.Payment;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorAdminTemplate.Application.DTOs.Memberships;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class MemberMembershipService : IMemberMembershipService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberCardEndpoint = "/MemberPaymentMembershipInfo";
        private const string MemberCancelMembershipEndpoint = "/MemberMembershipCancellation/cancel-membership";
        private const string MemberCancelMembershipCheckEndpoint = "/MemberMembershipCancellation/earliest-cancellation-date";


        public MemberMembershipService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }




       public async Task<MemberPaymentMembershipInfoResponseDTO> GetMembershipInfo()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberCardEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var membershipInfo = await response.Content.ReadFromJsonAsync<MemberPaymentMembershipInfoResponseDTO>();
                    return membershipInfo ?? new MemberPaymentMembershipInfoResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberPaymentMembershipInfoResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching membership info: {ex.Message}");
                return new MemberPaymentMembershipInfoResponseDTO();
            }
        } 
       
        public async Task<MemberCancelMembershipCheckResponseDTO> GetMemberMembershipCancelPolicy()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberCancelMembershipCheckEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var cancelPolicy = await response.Content.ReadFromJsonAsync<MemberCancelMembershipCheckResponseDTO>();
                    return cancelPolicy ?? new MemberCancelMembershipCheckResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberCancelMembershipCheckResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching membership info: {ex.Message}");
                return new MemberCancelMembershipCheckResponseDTO();
            }
        }
        
        public async Task<MemberCancelMembershipResponseDTO> MemberMembershipCancel(MemberCancelMembershipDTO memberCancelMembershipDto)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
        
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
                // Send POST request with DTO as body
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_apiConfiguration.BaseUrl}{MemberCancelMembershipEndpoint}", 
                    memberCancelMembershipDto);
        
                if (response.IsSuccessStatusCode)
                {
                    var cancelResponse = await response.Content.ReadFromJsonAsync<MemberCancelMembershipResponseDTO>();
                    return cancelResponse ?? new MemberCancelMembershipResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    // You might want to log the error message or throw an exception
                    Console.WriteLine($"Error: {errorMessage}");
                    return new MemberCancelMembershipResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling membership: {ex.Message}");
                // Consider throwing a custom exception or logging the full stack trace
                throw; // Re-throw the exception to propagate it
            }
        }

        
    }
}
