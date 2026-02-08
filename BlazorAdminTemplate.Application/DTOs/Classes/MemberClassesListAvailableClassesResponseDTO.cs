using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BlazorAdminTemplate.Domain.Entities;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class MemberClassesListAvailableClassesResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public string AppliedFilter { get; set; } = string.Empty;
        public string TargetOrganisationSubGUID { get; set; } = string.Empty;

        [JsonPropertyName("currentUserOrganisation")]
        public CurrentUserOrganisation? CurrentUserOrganisation { get; set; }

        [JsonPropertyName("allowedGuestOrganisations")]
        public List<GuestTrainingClasses> AllowedGuestOrganisations { get; set; } = new();

        [JsonPropertyName("availableTrainingClassTypes")]
        public List<Domain.Entities.ClassTypes> AvailableClassTypes { get; set; } = new();

        [JsonPropertyName("classes")]
        public List<TrainingClasses> AvailableClasses { get; set; } = new();

        // Helper property to convert single CurrentUserOrganisation to List for backward compatibility
        public List<CurrentUserOrganisation> CurrentUserOrganisations
        {
            get
            {
                return CurrentUserOrganisation != null
                    ? new List<CurrentUserOrganisation> { CurrentUserOrganisation }
                    : new List<CurrentUserOrganisation>();
            }
        }
    }
}