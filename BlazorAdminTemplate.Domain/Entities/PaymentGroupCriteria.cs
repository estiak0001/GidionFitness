using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class PaymentGroupCriteria
    {
        public string OrgMainGuid { get; set; } = string.Empty;
        public string OrgSubGuid { get; set; } = string.Empty;
    }
}
