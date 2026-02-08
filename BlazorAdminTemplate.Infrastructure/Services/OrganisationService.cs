using BlazorAdminTemplate.Application.DTOs.Organisations;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{

    public class OrganisationService : IOrganisationService
    {

        public readonly HttpClient _httpclient;
        public readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        public readonly string _mainOrganisationEndpoint = "/OrganisationMain/";
        public readonly string _organisationDomainEndpoint = "/OrganisationSite/lookup/";
        public readonly string _subOrganisationEndpoint = "/OrganisationSub/";
        public readonly string _OrganisationInfoEndpoint = "/OrganisationInfo/Get";
        public readonly string _changeOrganisationEndpoint = "/StaffChangeOrganisation/change";
        public string mainOrganisationGUID = string.Empty;

        public OrganisationService(HttpClient httpclient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpclient = httpclient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }



        public async Task<MainOrganisationResponseDTO> GetMainOrganisationAsync(string mainOrganisationGUID)
        {
            try
            {
                var response = await _httpclient.GetAsync($"{_apiConfiguration.BaseUrl}{_mainOrganisationEndpoint}{mainOrganisationGUID}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MainOrganisationResponseDTO>()
                           ?? new MainOrganisationResponseDTO();
                }
                else
                {
                    throw new Exception($"Failed to retrieve main organisation: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving main organisation: {ex.Message}");
                return new MainOrganisationResponseDTO();
            }
        }

        public async Task<OrganisationDomain> GetOrganisationByDomainAsync(string domain)
        {
            try
            {
                var response = await _httpclient.GetAsync($"{_apiConfiguration.BaseUrl}{_organisationDomainEndpoint}{domain}");
                if (response.IsSuccessStatusCode)
                {
                    var organisationDomain = await response.Content.ReadFromJsonAsync<OrganisationDomain>();
                    mainOrganisationGUID = organisationDomain.OrganisationSite.OrganisationMainGUID;
                    return organisationDomain ?? new OrganisationDomain();

                }
                else
                {
                    throw new Exception($"Failed to retrieve organisation by domain: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving organisation by domain: {ex.Message}");
                return new OrganisationDomain();
            }
        }

        public string GetCurrentMainOrganisationGUID()
        {
            return mainOrganisationGUID;
        }

        public async Task<SubOrganisationResponseDTO> GetSubOrganisationsAsync(string mainOrganisationGUID)
        {
            try 
            {
                var response = await _httpclient.GetAsync($"{_apiConfiguration.BaseUrl}{_subOrganisationEndpoint}{mainOrganisationGUID}");
                if (response.IsSuccessStatusCode)
                {
                   var result = await response.Content.ReadFromJsonAsync<SubOrganisationResponseDTO>()
                                ?? new SubOrganisationResponseDTO();

                    return result;

                }
                else
                {
                    throw new Exception($"Failed to retrieve sub organisations: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving sub organisations: {ex.Message}");
                return new SubOrganisationResponseDTO();
            }
        }

        public async Task<StaffChangeOrganisationResponseDTO> ChangeOrganisationAsync(StaffChangeOrganisationDTO request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpclient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{_changeOrganisationEndpoint}", request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<StaffChangeOrganisationResponseDTO>()
                           ?? new StaffChangeOrganisationResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Failed to change organisation: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing organisation: {ex.Message}");
                throw;
            }
        }
    }
}
