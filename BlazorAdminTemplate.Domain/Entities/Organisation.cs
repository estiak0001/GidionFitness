using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class Organisation
    {
        public string OrganisationMainGUID { get; set; } = string.Empty;
        public string OrganisationMainName { get; set; } = string.Empty;
        public string OrganisationMainAddress { get; set; } = string.Empty;
        public string OrganisationMainCity { get; set; } = string.Empty;
        public int OrganisationMainZipCode { get; set; } 
        public string OrganisationMainContactName { get; set; } = string.Empty;
        public string OrganisationMainContactPhone { get; set; } = string.Empty;
        public string OrganisationMainContactEmail { get; set; } = string.Empty;
        public string OrganisationMainContactComment { get; set; } = string.Empty;
        public string OrganisationMainJsonData { get; set; } = string.Empty;
        public bool OrganisationMainEnabled { get; set; } = true;
    }
}
