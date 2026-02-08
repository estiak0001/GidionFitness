using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class StaffTrainerDTO
    {
        [JsonPropertyName("TrainerName")]
        public string TrainerName { get; set; } = string.Empty;

        [JsonPropertyName("MemberGUID")]
        public string MemberGUID { get; set; } = string.Empty;
    }

    public class StaffTrainerListResponseDTO
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("data")]
        public List<StaffTrainerDTO> Data { get; set; } = new();
    }
}
