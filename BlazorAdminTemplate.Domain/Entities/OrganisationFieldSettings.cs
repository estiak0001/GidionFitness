using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class OrganisationFieldSettings
    {
        public int ShowMemberCPR { get; set; } = 0;
        public int ShowMemberSex { get; set; } = 0;
        public int ShowMemberHealthInsurance { get; set; } = 0;
    }
}
