using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class OrganisationDomain
    {
        public string Message { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public OrganisationSite OrganisationSite { get; set; } = new OrganisationSite();
    }
}
