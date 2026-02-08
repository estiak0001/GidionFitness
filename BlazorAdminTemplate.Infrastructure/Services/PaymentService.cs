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
    public class PaymentService : IPaymentService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberCreatePaymentEndpoint = "/PaymentFreePayPaymentWindow/create-payment";
        private const string MemberPaymentInfoEndpoint = "/MemberCalculateStartPayment";

        public PaymentService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }



        public async Task<MemberPaymentCreateResponseDTO> MemberCreatePaymentAsync(MemberPaymentCreateRequestDTO request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{MemberCreatePaymentEndpoint}", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MemberPaymentCreateResponseDTO>()
                           ?? new MemberPaymentCreateResponseDTO();
                }
                else
                {
                    throw new Exception($"Failed to create payment: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating payment: {ex.Message}");
                return new MemberPaymentCreateResponseDTO();
            }
        }

        public async Task<MemberPaymentInfoResponseDTO> MemberPaymentInfoAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberPaymentInfoEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MemberPaymentInfoResponseDTO>()
                           ?? new MemberPaymentInfoResponseDTO();
                }
                else
                {
                    throw new Exception($"Failed to retrieve payment info: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving payment info: {ex.Message}");
                return new MemberPaymentInfoResponseDTO();
            }
        }

       
    }
}
