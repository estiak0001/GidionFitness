using BlazorAdminTemplate.Application.DTOs.AccessControl;
using BlazorAdminTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlazorAdminTemplate.Infrastructure.Services;

public class AccessControlService : IAccessControlService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;
    private List<int>? _cachedAccessLevels;
    private List<AccessWebTypeDTO>? _cachedAccessWebTypes;
    private DateTime? _cacheExpiration;
    private readonly TimeSpan _cacheTimeout = TimeSpan.FromMinutes(10);

    public AccessControlService(
        AuthenticationStateProvider authenticationStateProvider,
        HttpClient httpClient,
        ITokenService tokenService)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    public List<int> GetUserAccessLevels()
    {
        // Return cached value if available
        if (_cachedAccessLevels != null)
        {
            return _cachedAccessLevels;
        }

        var authState = _authenticationStateProvider.GetAuthenticationStateAsync().Result;
        var user = authState.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            _cachedAccessLevels = new List<int>();
            return _cachedAccessLevels;
        }

        // Get the memberGroupWebAccessLevels claim
        var accessLevelsClaim = user.FindFirst("memberGroupWebAccessLevels")?.Value;

        if (string.IsNullOrEmpty(accessLevelsClaim))
        {
            _cachedAccessLevels = new List<int>();
            return _cachedAccessLevels;
        }

        try
        {
            // Parse the claim value - it can be either:
            // 1. A comma-separated string: "1,2,3,4,7,8,9"
            // 2. Or individual claims for each level
            var levels = new List<int>();

            // Try to parse as comma-separated
            if (accessLevelsClaim.Contains(','))
            {
                levels = accessLevelsClaim
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.TryParse(s.Trim(), out var level) ? level : 0)
                    .Where(l => l > 0)
                    .ToList();
            }
            else
            {
                // Single value or multiple claims with same name
                var allAccessLevelClaims = user.FindAll("memberGroupWebAccessLevels");
                levels = allAccessLevelClaims
                    .Select(c => int.TryParse(c.Value, out var level) ? level : 0)
                    .Where(l => l > 0)
                    .Distinct()
                    .ToList();
            }

            _cachedAccessLevels = levels;
            return _cachedAccessLevels;
        }
        catch
        {
            _cachedAccessLevels = new List<int>();
            return _cachedAccessLevels;
        }
    }

    public bool HasAccessLevel(int level)
    {
        var userLevels = GetUserAccessLevels();
        return userLevels.Contains(level);
    }

    public bool HasAnyAccessLevel(params int[] levels)
    {
        if (levels == null || levels.Length == 0)
        {
            return false;
        }

        var userLevels = GetUserAccessLevels();
        return levels.Any(level => userLevels.Contains(level));
    }

    public async Task<List<AccessWebTypeDTO>> GetAllAccessWebTypesAsync()
    {
        // Return cached data if still valid
        if (_cachedAccessWebTypes != null && 
            _cacheExpiration.HasValue && 
            DateTime.Now < _cacheExpiration.Value)
        {
            return _cachedAccessWebTypes;
        }

        try
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return new List<AccessWebTypeDTO>();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await _httpClient.GetFromJsonAsync<AccessWebTypesResponseDTO>(
                "https://fitnessapi.gidion.dk/api/StaffAccessManagementStaffGroup/access-web-types");

            if (response?.AccessWebTypes != null)
            {
                _cachedAccessWebTypes = response.AccessWebTypes;
                _cacheExpiration = DateTime.Now.Add(_cacheTimeout);
                return _cachedAccessWebTypes;
            }

            return new List<AccessWebTypeDTO>();
        }
        catch
        {
            return _cachedAccessWebTypes ?? new List<AccessWebTypeDTO>();
        }
    }

    public async Task<AccessWebTypeDTO?> GetAccessWebTypeByLevelAsync(int level)
    {
        var allTypes = await GetAllAccessWebTypesAsync();
        return allTypes.FirstOrDefault(t => t.AccessWebTypesAccessLevel == level);
    }
}
