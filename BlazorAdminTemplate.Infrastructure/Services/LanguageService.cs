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
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly string _languagesEndpoint = "/MemberLanguage/languages";

        public LanguageService(HttpClient httpClient, ApiConfiguration apiConfiguration)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
        }


        public async Task<MemberLanguageResponseDTO> GetAllLanguagesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{_languagesEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberLanguageResponseDTO>()
                        ?? new MemberLanguageResponseDTO();

                    return result;
                }
                else
                {
                  throw new Exception($"Failed to retrieve languages: {response.ReasonPhrase}");

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching languages: {ex.Message}");
                return new MemberLanguageResponseDTO();
                
            }
        }
    }
}
