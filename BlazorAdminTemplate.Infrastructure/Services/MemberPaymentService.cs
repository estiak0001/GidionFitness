using BlazorAdminTemplate.Application.DTOs.Payment;
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
    public class MemberPaymentService : IMemberPaymentService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberPaymentListEndpoint = "/MemberPaymentListPayments";
        

        public MemberPaymentService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<MemberPaymentListReponseDTO> GetMemberPaymentsAsync(int page)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberPaymentListEndpoint}?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    var paymentList = await response.Content.ReadFromJsonAsync<MemberPaymentListReponseDTO>();
                    return paymentList ?? new MemberPaymentListReponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberPaymentListReponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member payments: {ex.Message}");
                return new MemberPaymentListReponseDTO();
            }
        }

        
    }
}
