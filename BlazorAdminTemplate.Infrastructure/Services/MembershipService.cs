using BlazorAdminTemplate.Application.DTOs.Memberships;
using BlazorAdminTemplate.Application.DTOs.Training;
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
    public class MembershipService : IMembershipService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberListEndpoint = "/StaffMemberList";
        private const string UpdateMemberEndpoint = "/Auth/update-member/";
        private const string GetSpecificMemberEndpoint = "/Auth/member/";
        private const string GetMembershipListEndpoint = "/MemberMembershipListAvalible/ListAvaliblePaymentGroups";


        public MembershipService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<MembershipsResponseDTO> GetMembershipListAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{GetMembershipListEndpoint}";

                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var memberList = await response.Content.ReadFromJsonAsync<MembershipsResponseDTO>();
                    return memberList ?? new MembershipsResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MembershipsResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member list: {ex.Message}");
                return new MembershipsResponseDTO();
            }
        }

    }
}
