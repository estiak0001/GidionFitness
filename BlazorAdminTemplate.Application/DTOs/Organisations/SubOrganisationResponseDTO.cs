using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Organisations
{
    public class SubOrganisationResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public string OrgMainGuid { get; set; } = string.Empty;
        public List<SubOrganisation> OrganisationSubs { get; set; } = new List<SubOrganisation>();
    }
}
