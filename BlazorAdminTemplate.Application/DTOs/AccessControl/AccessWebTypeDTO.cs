using System.Text.Json.Serialization;

namespace BlazorAdminTemplate.Application.DTOs.AccessControl;

public class AccessWebTypeDTO
{
    [JsonPropertyName("accessWebTypesGUID")]
    public string AccessWebTypesGUID { get; set; } = string.Empty;

    [JsonPropertyName("accessWebTypesAccessLevel")]
    public int AccessWebTypesAccessLevel { get; set; }

    [JsonPropertyName("accessWebTypesAccessLevelName")]
    public string AccessWebTypesAccessLevelName { get; set; } = string.Empty;

    [JsonPropertyName("accessWebTypesAccessLevelDescription")]
    public string AccessWebTypesAccessLevelDescription { get; set; } = string.Empty;
}

public class AccessWebTypesResponseDTO
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("accessWebTypes")]
    public List<AccessWebTypeDTO> AccessWebTypes { get; set; } = new();
}
