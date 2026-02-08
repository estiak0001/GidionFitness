using BlazorAdminTemplate.Application.DTOs;
using BlazorAdminTemplate.Application.DTOs.NewsDTO;
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
    public class NewsService : INewsService
    {

        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly ApiConfiguration _apiConfiguration;
        private const string MemberNewsShowListEndpoint = "/MemberNewsShowList";
        private const string SpecificMemberNewsEndpoint = "/MemberNewsShow";
        private const string TopMemberNewsListEndpoint = "/MemberNewsShowListTop";
        private const string StaffCreateNewsEndpoint = "/StaffNews/create";
        private const string StaffEditNewsEndpoint = "/StaffNews/edit";
        private const string StaffDeleteNewsEndpoint = "/StaffNews/delete";
        private const string StaffNewsEndpoint = "/StaffNews";


        public NewsService(HttpClient httpClient, ITokenService tokenService, ApiConfiguration apiConfiguration)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _apiConfiguration = apiConfiguration;
        }




        public async Task<MemberNewsShowListResponseDTO> GetMemberNewsShowListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{MemberNewsShowListEndpoint}?page={pageNumber}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var newsList = await response.Content.ReadFromJsonAsync<MemberNewsShowListResponseDTO>();
                    return newsList ?? new MemberNewsShowListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberNewsShowListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member news list: {ex.Message}");
                return new MemberNewsShowListResponseDTO();
            }
        }

        public async Task<MemberNewsShowResponseDTO> GetSpecificMemberNewsAsync(string memberNewsGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{SpecificMemberNewsEndpoint}/{memberNewsGuid}");
                if (response.IsSuccessStatusCode)
                {
                    var newsItem = await response.Content.ReadFromJsonAsync<MemberNewsShowResponseDTO>();
                    return newsItem ?? new MemberNewsShowResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberNewsShowResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching specific member news: {ex.Message}");
                return new MemberNewsShowResponseDTO();
            }
        }

        public async Task<MemberNewsShowListTopResponseDTO> GetTopMemberNewsListAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{TopMemberNewsListEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var topNewsList = await response.Content.ReadFromJsonAsync<MemberNewsShowListTopResponseDTO>();
                    return topNewsList ?? new MemberNewsShowListTopResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberNewsShowListTopResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching top member news list: {ex.Message}");
                return new MemberNewsShowListTopResponseDTO();
            }
        }

        public async Task<StaffCreateNewsResponseDTO> CreateStaffNewsAsync(StaffCreateNewsRequestDTO newsRequest)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffCreateNewsEndpoint}", newsRequest);

                if (response.IsSuccessStatusCode)
                {
                    var createResponse = await response.Content.ReadFromJsonAsync<StaffCreateNewsResponseDTO>();
                    return createResponse ?? new StaffCreateNewsResponseDTO { Message = "News created successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffCreateNewsResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating staff news: {ex.Message}");
                return new StaffCreateNewsResponseDTO { Message = $"Exception: {ex.Message}" };
            }
        }

        public async Task<StaffEditNewsReponseDTO> EditStaffNewsAsync(StaffEditNewsRequestDTO newsRequest)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffEditNewsEndpoint}", newsRequest);
                if (response.IsSuccessStatusCode)
                {
                    var editResponse = await response.Content.ReadFromJsonAsync<StaffEditNewsReponseDTO>();
                    return editResponse ?? new StaffEditNewsReponseDTO { Message = "News edited successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffEditNewsReponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing staff news: {ex.Message}");
                return new StaffEditNewsReponseDTO { Message = $"Exception: {ex.Message}" };
            }
        }

        public async Task<StaffDeleteNewsResponseDTO> DeleteStaffNewsAsync(StaffDeleteNewsRequestDTO newsRequest)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                using var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiConfiguration.BaseUrl}{StaffDeleteNewsEndpoint}")
                {
                    Content = JsonContent.Create(newsRequest)
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var deleteResponse = await response.Content.ReadFromJsonAsync<StaffDeleteNewsResponseDTO>();
                    return deleteResponse ?? new StaffDeleteNewsResponseDTO { Message = "News deleted successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffDeleteNewsResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting staff news: {ex.Message}");
                return new StaffDeleteNewsResponseDTO { Message = $"Exception: {ex.Message}" };
            }
        }


        #region ResponseDTO

        public async Task<ResponseDTO<List<StaffNewsDTO>>> GetStaffNewsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffNewsEndpoint}?page={pageNumber}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var newsList = await response.Content.ReadFromJsonAsync< ResponseDTO<List<StaffNewsDTO>>>();
                    newsList?.Initialize();
                    return newsList ?? new ResponseDTO<List<StaffNewsDTO>>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<List<StaffNewsDTO>>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching member news list: {ex.Message}");
                return new ResponseDTO<List<StaffNewsDTO>>();
            }



        }

        public async Task<ResponseDTO<StaffNewsDTO>> CreateStaffNewAsync(StaffNewsDTO news)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffCreateNewsEndpoint}", news);

                if (response.IsSuccessStatusCode)
                {
                    var createResponse = await response.Content.ReadFromJsonAsync<ResponseDTO<StaffNewsDTO>>();
                    return createResponse ?? new ResponseDTO<StaffNewsDTO> { Message = "News created successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<StaffNewsDTO> { Message = $"Error: {errorMessage}" };
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating staff news: {ex.Message}");
                return new ResponseDTO<StaffNewsDTO> { Message = $"Exception: {ex.Message}" };
            }
        }

        public async Task<ResponseDTO<StaffNewsDTO>> UpdateStaffNewAsync(StaffNewsDTO news)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffEditNewsEndpoint}", news);
                if (response.IsSuccessStatusCode)
                {
                    var editResponse = await response.Content.ReadFromJsonAsync<ResponseDTO<StaffNewsDTO>>();
                    return editResponse ?? new ResponseDTO<StaffNewsDTO> { Message = "News edited successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<StaffNewsDTO> { Message = $"Error: {errorMessage}" };
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing staff news: {ex.Message}");
                return new ResponseDTO<StaffNewsDTO> { Message = $"Exception: {ex.Message}" };
            }



        }

        public async Task<ResponseDTO<StaffNewsDTO>> DeleteStaffNewAsync(StaffNewsDTO news)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{StaffDeleteNewsEndpoint}?memberNewsGUID={news.MemberNewsGUID}");

                if (response.IsSuccessStatusCode)
                {
                    var deleteResponse = await response.Content.ReadFromJsonAsync<ResponseDTO<StaffNewsDTO>>();
                    return deleteResponse ?? new ResponseDTO<StaffNewsDTO> { Message = "News deleted successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<StaffNewsDTO> { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting staff news: {ex.Message}");
                return new ResponseDTO<StaffNewsDTO> { Message = $"Exception: {ex.Message}" };
            }
        }

        #endregion

    }
}
