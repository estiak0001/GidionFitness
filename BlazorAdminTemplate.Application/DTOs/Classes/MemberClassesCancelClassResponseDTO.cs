using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class MemberClassesCancelClassResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string? CancelledEnrollmentGUID { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassClassesGuid")]
        public string? TrainingClassGUID { get; set; } = string.Empty;
        public DateTime? CancellationDateTime { get; set; }
        public CancelClassDetails? ClassDetails { get; set; } 

        public int? MaxCancellationTime { get; set; }
        public int? MinutesBeforeClass { get; set; }
        public DateTime? ClassStartDate { get; set; }


        [JsonIgnore]
        public bool IsSuccess => !string.IsNullOrEmpty(CancelledEnrollmentGUID);

        [JsonIgnore]
        public bool IsCancellationNotAllowed => MaxCancellationTime.HasValue;
    }
}
