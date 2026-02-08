using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class SubOrganisation
    {
        public string OrganisationSubGUID { get; set; } = string.Empty;
        public string OrganisationMainGUID { get; set; } = string.Empty;
        public string OrganisationSubName { get; set; } = string.Empty;
        public string OrganisationSubAllowGuest { get; set; } = string.Empty;
        public string OrganisationSubAddress { get; set; } = string.Empty;
        public string OrganisationSubCity { get; set; } = string.Empty;
        public int OrganisationSubZipCode { get; set; }
        public string OrganisationSubContactName { get; set; } = string.Empty;
        public string OrganisationSubContactPhone { get; set; } = string.Empty;
        public string OrganisationSubContactEmail { get; set; } = string.Empty;
        public string OrganisationSubContactComment { get; set; } = string.Empty;
        public string OrganisationMainName { get; set; } = string.Empty;
    }
}
