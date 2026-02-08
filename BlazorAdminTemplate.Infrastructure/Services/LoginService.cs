using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class LoginService : ILoginService
    {

        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly ApiConfiguration _apiConfiguration;
        private const string AuthLoginEndpoint = "Auth/login";
        private const string AuthLogoutEndpoint = "Auth/logout";
        private const string RefreshTokenEndpoint = "Auth/refresh";

        public event Action<bool>? AuthenticationStateChanged;


        public LoginService(HttpClient httpClient, ITokenService tokenService, ApiConfiguration apiConfiguration)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _apiConfiguration = apiConfiguration;
        }


        public async Task<MemberDTO?> GetCurrentMemberAsync()
        {
            if (await _tokenService.IsTokenExpiringSoonAsync())
            {
               
                var refreshResponse = await RefreshTokenAsync();
                if (refreshResponse == null)
                {
                    
                    await LogoutAsync();
                    return null;
                }
            }
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var memberDto = new MemberDTO
                {
                    MemberGUID = jwt.Claims.FirstOrDefault(c => c.Type == "MemberGUID")?.Value,
                    MemberEmail = jwt.Claims.FirstOrDefault(c => c.Type == "MemberEmail")?.Value,
                    MemberFirstName = jwt.Claims.FirstOrDefault(c => c.Type == "MemberFirstName")?.Value,
                    MemberLastName = jwt.Claims.FirstOrDefault(c => c.Type == "MemberLastName")?.Value,
                    MemberPhone = jwt.Claims.FirstOrDefault(c => c.Type == "MemberPhone")?.Value,
                    MemberType = jwt.Claims.FirstOrDefault(c => c.Type == "MemberType")?.Value,
                };

                // Handle MemberCreatedAt parsing more robustly
                var createdAtClaim = jwt.Claims.FirstOrDefault(c => c.Type == "MemberCreatedAt" || c.Type == "iat")?.Value;
                if (!string.IsNullOrEmpty(createdAtClaim))
                {
                    if (DateTime.TryParse(createdAtClaim, out var createdAt))
                    {
                        // If the parsed date is in UTC, convert to local time for display
                        if (createdAt.Kind == DateTimeKind.Utc)
                        {
                            memberDto.MemberCreatedAt = createdAt.ToLocalTime();
                        }
                        else
                        {
                            memberDto.MemberCreatedAt = createdAt;
                        }
                    }
                    else if (long.TryParse(createdAtClaim, out var unixTime))
                    {
                        // Handle Unix timestamp - convert to local time
                        memberDto.MemberCreatedAt = DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime;
                    }
                    else
                    {
                        Console.WriteLine($"LoginService: Could not parse MemberCreatedAt: {createdAtClaim}");
                        memberDto.MemberCreatedAt = DateTime.Now; // Use local time
                    }
                }
                else
                {
                    memberDto.MemberCreatedAt = DateTime.Now; // Use local time
                }

                // If we still don't have essential data, try to extract from other standard claims
                if (string.IsNullOrEmpty(memberDto.MemberGUID) && string.IsNullOrEmpty(memberDto.MemberEmail))
                {
                    // Try to get name from standard "name" claim if first/last names are empty
                    if (string.IsNullOrEmpty(memberDto.MemberFirstName) && string.IsNullOrEmpty(memberDto.MemberLastName))
                    {
                        var fullName = GetClaimValue(jwt, "name", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
                        if (!string.IsNullOrEmpty(fullName))
                        {
                            var nameParts = fullName.Split(' ', 2);
                            memberDto.MemberFirstName = nameParts[0];
                            if (nameParts.Length > 1)
                                memberDto.MemberLastName = nameParts[1];
                        }
                    }

                 
                    
                }
                    return memberDto;
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing token: {ex.Message}");
                return null;
            }
        }

        private string GetClaimValue(JwtSecurityToken jwt, params string[] claimNames)
        {
            foreach (var claimName in claimNames)
            {
                var claim = jwt.Claims.FirstOrDefault(c => c.Type.Equals(claimName, StringComparison.OrdinalIgnoreCase));
                if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
                {
                    
                    return claim.Value;
                }
            }
           
            return string.Empty;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _tokenService.GetTokenAsync();
        }

        public async Task<bool> IsAuthenticatedAsync()
        {

            return await _tokenService.IsTokenValidAsync();
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            try 
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}/{AuthLoginEndpoint}", request);
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.AccessToken))
                    {
                        await _tokenService.StoreTokenAsync(loginResponse.AccessToken);
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResponse.AccessToken);

                        await _tokenService.StoreRefreshToken(loginResponse.RefreshToken);

                        await _tokenService.GetTokenDebugInfoAsync();

                        AuthenticationStateChanged?.Invoke(true);
                        
                    }
                    return loginResponse ?? new LoginResponseDTO();
                }
                return new LoginResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                return new LoginResponseDTO();
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}/{AuthLogoutEndpoint}", new { });
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Logout successful.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server logout failed: {ex.Message}");
            }
            finally
            {
                await _tokenService.RemoveTokenAsync();
                await _tokenService.RemoveRefreshTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = null;

                // Notify subscribers that authentication state changed
                AuthenticationStateChanged?.Invoke(false);
                Console.WriteLine("Logout successful.");
            }


           

            return true;
        }

        public async Task<RefreshTokenResponseDTO?> RefreshTokenAsync()
        {
            try 
            {
                var refreshToken = await _tokenService.GetRefreshTokenAsync();
                if(string.IsNullOrEmpty(refreshToken))
                {
                    return null; // No refresh token available
                }

                var refreshRequest = new RefreshTokenRequestDTO
                {
                    RefreshToken = refreshToken
                };

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}/{RefreshTokenEndpoint}", refreshRequest);

                if (response.IsSuccessStatusCode)
                {
                    var refreshResponse = await response.Content.ReadFromJsonAsync<RefreshTokenResponseDTO>();
                    if (refreshResponse != null && !string.IsNullOrEmpty(refreshResponse.AccessToken))
                    {
                        await _tokenService.StoreTokenAsync(refreshResponse.AccessToken);
                        await _tokenService.StoreRefreshToken(refreshResponse.RefreshToken);

                        _httpClient.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", refreshResponse.AccessToken);

                    
                        return refreshResponse;
                    }
                }
                else 
                {
                    Console.WriteLine($"Refresh token failed: {response.StatusCode} - {response.ReasonPhrase}");
                    await LogoutAsync();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Refresh token failed: {ex.Message}");
                return null;
            }
        }
    }
}
