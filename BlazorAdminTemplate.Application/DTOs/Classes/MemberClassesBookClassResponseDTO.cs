using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class MemberClassesBookClassResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string MemberClassesBookClassGUID { get; set; } = string.Empty;
        public string TrainingClassClassesGUID { get; set; } = string.Empty;

        [JsonPropertyName("MemberClassesBookClassDetails")]
        public List<BookClassDetails> BookClassDetails { get; set; } = new();
    }
}
