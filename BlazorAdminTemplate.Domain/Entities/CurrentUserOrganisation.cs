using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class CurrentUserOrganisation
    {
        public string OrganisationSubGUID { get; set; } = string.Empty;
        public string OrganisationSubName { get; set; } = string.Empty;
    }
}
