using BlazorAdminTemplate.Application.DTOs.Members;
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
    public class MemberService : IMemberService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string MemberListEndpoint = "/StaffMemberList";
        private const string UpdateMemberEndpoint = "/Auth/update-member/";
        private const string GetSpecificMemberEndpoint = "/Auth/member/";
    

        public MemberService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<StaffMemberListResponseDTO> GetMemberListAsync(int page, int pageSize, string search, string searchMembership)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{MemberListEndpoint}?page={page}&pageSize={pageSize}&search={search}";

                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var memberList = await response.Content.ReadFromJsonAsync<StaffMemberListResponseDTO>();
                    return memberList ?? new StaffMemberListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffMemberListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member list: {ex.Message}");
                return new StaffMemberListResponseDTO();
            }
        }

        public async Task<StaffUpdateMemberResponseDTO> UpdateMemberAsync(StaffUpdateMemberRequestDTO request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{UpdateMemberEndpoint}{request.MemberGuid}", request);
                if (response.IsSuccessStatusCode)
                {
                    var updateResponse = await response.Content.ReadFromJsonAsync<StaffUpdateMemberResponseDTO>();
                    return updateResponse ?? new StaffUpdateMemberResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffUpdateMemberResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating member: {ex.Message}");
                return new StaffUpdateMemberResponseDTO();
            }
        }

        public async Task<StaffGetSpecificMemberResponseDTO> GetSpecificMemberAsync(string memberGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{GetSpecificMemberEndpoint}{memberGuid}");
                if (response.IsSuccessStatusCode)
                {
                    var memberDetails = await response.Content.ReadFromJsonAsync<StaffGetSpecificMemberResponseDTO>();
                    return memberDetails ?? new StaffGetSpecificMemberResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffGetSpecificMemberResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member details: {ex.Message}");
                return new StaffGetSpecificMemberResponseDTO();
            }
        }

        
    }
}
