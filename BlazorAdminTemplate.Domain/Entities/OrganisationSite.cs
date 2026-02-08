using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class OrganisationSite
    {
        public string OrganisationSiteGUID { get; set; } = string.Empty;
        public string OrganisationMainGUID { get; set; } = string.Empty;
        public string OrganisationSubGUID { get; set; } = string.Empty;
        public string OrganisationSiteDomain { get; set; } = string.Empty;
        public string OrganisationWebName { get; set; } = string.Empty;
        public string OrganisationWebDescription { get; set; } = string.Empty;
        public string OrganisationSubJsonData { get; set; } = string.Empty;
        public string OrganisationSubAddress { get; set; } = string.Empty;
        public string OrganisationSubCity { get; set; } = string.Empty;
        public int OrganisationSubZipCode { get; set; } = 0;
        public string OrganisationSubContactName { get; set; } = string.Empty;
        public string OrganisationSubContactPhone { get; set; } = string.Empty;
        public string OrganisationSubContactEmail { get; set; } = string.Empty;
        public string OrganisationSubContactComment { get; set; } = string.Empty;
    }
}
