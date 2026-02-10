using BlazorAdminTemplate.Application.DTOs.AccessControl;

namespace BlazorAdminTemplate.Application.Interfaces;

public interface IAccessControlService
{
    /// <summary>
    /// Get all access levels for the current user from JWT token
    /// </summary>
    /// <returns>List of access level numbers the user has</returns>
    List<int> GetUserAccessLevels();

    /// <summary>
    /// Check if the current user has a specific access level
    /// </summary>
    /// <param name="level">The access level number to check</param>
    /// <returns>True if user has the access level, false otherwise</returns>
    bool HasAccessLevel(int level);

    /// <summary>
    /// Check if the current user has any of the specified access levels
    /// </summary>
    /// <param name="levels">List of access level numbers</param>
    /// <returns>True if user has at least one of the access levels, false otherwise</returns>
    bool HasAnyAccessLevel(params int[] levels);

    /// <summary>
    /// Get all available access web types from API
    /// </summary>
    /// <returns>List of all access web types with their levels, names, and descriptions</returns>
    Task<List<AccessWebTypeDTO>> GetAllAccessWebTypesAsync();

    /// <summary>
    /// Get access web type information for a specific access level
    /// </summary>
    /// <param name="level">The access level number</param>
    /// <returns>Access web type information or null if not found</returns>
    Task<AccessWebTypeDTO?> GetAccessWebTypeByLevelAsync(int level);
}
