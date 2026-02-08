using BlazorAdminTemplate.Application.DTOs.Classes;
using BlazorAdminTemplate.Application.DTOs.Templates;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using Flurl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string StaffGetMailTemplatesEndpoint = "/StaffTemplates/MailTemplateList";
        private const string StaffGetMailTemplateEndpoint = "/StaffTemplates/MailTemplateGet";
        private const string StaffEditMailTemplateEndpoint = "/StaffTemplates/MailTemplateEdit";
        private const string StaffGetMailTemplatePlaceholdersEndpoint = "/StaffTemplatesPlaceholders/TemplatesPlaceholders";


        public TemplateService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<TemplateListResponseDTO> StaffGetMailTemplatesAsync(TemplateDTO query)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetMailTemplatesEndpoint}"
                    .SetQueryParams(new
                    {
                        page = query.Page,
                        pageSize = query.PageSize,
                        search=query.Search
                    });


                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TemplateListResponseDTO>();
                    return result ?? new TemplateListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching mail templates: {errorMessage}");
                    return new TemplateListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching mail templates: {ex.Message}");
                return new TemplateListResponseDTO();
            }
        }

        public async Task<TemplateResponseDTO> StaffGetMailTemplateAsync(string templateGUID)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetMailTemplateEndpoint}/{templateGUID}";
                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TemplateResponseDTO>();
                    return result ?? new TemplateResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching mail templates: {errorMessage}");
                    return new TemplateResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching mail templates: {ex.Message}");
                return new TemplateResponseDTO();
            }
        }

        public async Task<TemplatePlaceholderListResponseDTO> StaffGetMailTemplatePlaceholdersAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetMailTemplatePlaceholdersEndpoint}";

                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TemplatePlaceholderListResponseDTO>();
                    return result ?? new TemplatePlaceholderListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching mail template placeholders: {errorMessage}");
                    return new TemplatePlaceholderListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching mail template placeholders: {ex.Message}");
                return new TemplatePlaceholderListResponseDTO();
            }
        }

        public async Task<TemplateActionResponseDTO> StaffEditMailTemplateAsync(TemplateEditDTO template)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffEditMailTemplateEndpoint}";

                var requestUrl = url;
                var response = await _httpClient.PutAsJsonAsync(requestUrl, template);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TemplateActionResponseDTO>();
                    if (result != null)
                    {
                        result.Success = true;
                    }
                    return result ?? new TemplateActionResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error updating template: {errorMessage}");
                    return new TemplateActionResponseDTO { Success = false, Message = errorMessage};
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating template: {ex.Message}");
                return new TemplateActionResponseDTO { Success = false, Message = ex.Message};
            }
        }
    }
}
