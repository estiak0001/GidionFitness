using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Organisations
{
    public class StaffChangeOrganisationDTO
    {
        public string NewOrganisationSubGUID { get; set; } = string.Empty;
    }

    public class StaffChangeOrganisationResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}
