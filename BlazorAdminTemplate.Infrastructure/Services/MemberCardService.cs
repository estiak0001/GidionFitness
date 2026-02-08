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
    public class MemberCardService : IMemberCardService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberCardEndpoint = "/MemberPaymentCardInfo";
        private const string MemberAddnewCardEndpoint = "/PaymentFreePayPaymentWindow/create-payment-new-card";
        private const string DeleteCardEndpoint = "/MemberPaymentCardInfo/card/";
        public MemberCardService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<MemberPaymentCardInfoResponseDTO> GetPaymentCardInfoAsync()
        {
            try 
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberCardEndpoint}/cards");
                if (response.IsSuccessStatusCode)
                {
                    var cardInfo = await response.Content.ReadFromJsonAsync<MemberPaymentCardInfoResponseDTO>();
                    return cardInfo ?? new MemberPaymentCardInfoResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberPaymentCardInfoResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching payment card info: {ex.Message}");
                return new MemberPaymentCardInfoResponseDTO();
            }
        }

        public async Task<bool> DeletePaymentCardAsync(string cardGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{DeleteCardEndpoint}{cardGuid}");
                if (response.IsSuccessStatusCode) 
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to delete card: {errorMessage}");
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting payment card: {ex.Message}");
                return false;
            }
        }

        public async Task<MemberPaymentCreateResponseDTO> AddNewPaymentCardAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsync($"{_apiConfiguration.BaseUrl}{MemberAddnewCardEndpoint}", null);
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
    }
}
