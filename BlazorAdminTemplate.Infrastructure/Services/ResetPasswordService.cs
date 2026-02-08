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
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string ResetPasswordEndpoint = "/PasswordReset";
        private const string ChangePasswordEndpoint = "/Auth/change-password";

        public ResetPasswordService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiConfiguration = apiConfiguration ?? throw new ArgumentNullException(nameof(apiConfiguration));
            _tokenService = tokenService;
        }



        public async Task<ForgotPasswordResponseDTO> SendEmailAsync(ForgotPasswordRequestDTO request)
        {
            try 
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{ResetPasswordEndpoint}/request", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ForgotPasswordResponseDTO>();
                    Console.WriteLine("ForgotPasswordEmailPost successful" + result);
                    return result;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                    return new ForgotPasswordResponseDTO(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ForgotPasswordEmailPost failed: {ex.Message}");
                return new ForgotPasswordResponseDTO();
            }
        }

        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            try 
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{ResetPasswordEndpoint}/reset", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResetPasswordResponseDTO>();
                    Console.WriteLine("ResetPasswordPost successful" + result);
                    return result;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                    return new ResetPasswordResponseDTO(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ResetPasswordPost failed: {ex.Message}");
                return new ResetPasswordResponseDTO();
            }
        }

        public async Task<ChangePasswordResponseDTO> ChangePasswordAsync(ChangePasswordRequestDTO request)
        {
            try
            {
               

                // Get authentication token
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                  
                    return new ChangePasswordResponseDTO { Message = "Authentication required. Please log in again." };
                }

               

                // Set authorization header
                if (_httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{ChangePasswordEndpoint}", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ChangePasswordResponseDTO>();
                    return result ?? new ChangePasswordResponseDTO { Message = "Password changed successfully." };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    // Try to parse error response for better user feedback
                    var errorResponse = "Failed to change password. Please check your current password and try again.";
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        errorResponse = "Invalid current password or new password does not meet requirements.";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        errorResponse = "Authentication failed. Please log in again.";
                    }

                   
                    return new ChangePasswordResponseDTO { Message = errorResponse };
                }
            }
            catch (Exception ex)
            {
                
                return new ChangePasswordResponseDTO { Message = "An error occurred while changing your password. Please try again." };
            }
        }
    }
}
