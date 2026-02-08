using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {

        private readonly ITokenService _tokenService;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly HttpClient _httpClient;
        private const string GetProfileEndpoint = "/Auth/memberinfo";
        private const string UpdateProfileEndpoint = "/Auth/update-profile";
        private const string GetProfileImageEndpoint = "/ImageUpload/my-image";

        public ProfileService(ITokenService tokenService, ApiConfiguration apiConfiguration, HttpClient httpClient)
        {
            _tokenService = tokenService;
            _apiConfiguration = apiConfiguration;
            _httpClient = httpClient;

        }


        public async Task<ProfileResponseDTO?> GetProfileAsync()
        {
            try
            {

                Console.WriteLine("ProfileService: GetProfileAsync started");

                var token = await _tokenService.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                if (_httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.GetAsync(_apiConfiguration.BaseUrl + GetProfileEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var profileResponse = await response.Content.ReadFromJsonAsync<ProfileResponseDTO>();

                    if (profileResponse != null)
                    {
                        Console.WriteLine($"ProfileService: Profile loaded successfully - Name: {profileResponse.MemberFirstName}, Email: {profileResponse.MemberEmail}");

                        return profileResponse;
                    }
                    else
                    {
                        Console.WriteLine("ProfileService: Received null response from API");
                    }
                }
                else
                {
                    Console.WriteLine($"ProfileService: API call failed - Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ProfileService: Error response: {errorContent}");
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ProfileService: Error getting profile: {ex.Message}");
                return null;
            }
        }

        public async Task<string?> GetProfileImageUrlAsync()
        {
            try
            {
                

                var token = await _tokenService.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("ProfileService: No token available");
                    return null;
                }

                if (_httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var imageInfoEndpoint = $"{_apiConfiguration.BaseUrl}{GetProfileImageEndpoint}";
                Console.WriteLine($"ProfileService: Fetching image info from: {imageInfoEndpoint}");

                var response = await _httpClient.GetAsync(imageInfoEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ProfileService: Image info response: {responseContent}");

                    var imageInfo = JsonSerializer.Deserialize<JsonElement>(responseContent);

                    if (imageInfo.TryGetProperty("hasImage", out var hasImageProperty) && hasImageProperty.GetBoolean() &&
                        imageInfo.TryGetProperty("imageUrl", out var imageUrlProperty))
                    {
                        var imageUrl = imageUrlProperty.GetString();
                        Console.WriteLine($"ProfileService: Returning imageUrl: {imageUrl}");
                        return imageUrl;
                    }
                    else
                    {
                        Console.WriteLine("ProfileService: Member has no image (hasImage: false)");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"ProfileService: GetProfileImageInfo failed - Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ProfileService: Error response: {errorContent}");
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ProfileService: Error getting profile image info: {ex.Message}");
                return null;
            }
        }

        

        public async Task<bool> UpdateProfileAsync(UpdateProfileRequestDTO profile)
        {
            try
            {
                Console.WriteLine("ProfileService: UpdateProfileAsync started");

                var token = await _tokenService.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("ProfileService: No token available");
                    return false;
                }

                if (_httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }



                Console.WriteLine($"ProfileService: Making update API call to {_apiConfiguration.BaseUrl + UpdateProfileEndpoint}");
                var response = await _httpClient.PutAsJsonAsync(_apiConfiguration.BaseUrl + UpdateProfileEndpoint, profile);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("ProfileService: Profile updated successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine($"ProfileService: Update failed - Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ProfileService: Error response: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ProfileService: Error updating profile: {ex.Message}");
                return false;
            }
        }
        private DateTime? TryParseDate(string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            // Try different date formats that might be returned from the API
            var formats = new[]
            {
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHH:mm:ss.fff",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ssZ",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd",
                "MM/dd/yyyy",
                "dd/MM/yyyy"
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out var date))
                {
                    return date;
                }
            }

            // Fallback to general parsing
            if (DateTime.TryParse(dateString, out var fallbackDate))
            {
                return fallbackDate;
            }

            Console.WriteLine($"ProfileService: Could not parse date string: {dateString}");
            return null;
        }

        public async Task<bool> DeleteProfileImageAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("ProfileService: No token available");
                    return false;
                }
                if (_httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                var deleteImageEndpoint = $"{_apiConfiguration.BaseUrl}/ImageUpload/my-image";
                Console.WriteLine($"ProfileService: Deleting profile image via: {deleteImageEndpoint}");
                var response = await _httpClient.DeleteAsync(deleteImageEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("ProfileService: Profile image deleted successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine($"ProfileService: DeleteProfileImage failed - Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ProfileService: Error response: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ProfileService: Error deleting profile image: {ex.Message}");
                return false;
            }
        }
    }
}