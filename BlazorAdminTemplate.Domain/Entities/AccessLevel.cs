using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class AccessLevel
    {
        public string AccessControllerAccessGroupsControllerLevelGUID { get; set; } = string.Empty;
        public int AccessControllerAccessGroupsControllerLevel { get; set; }
        public string AccessControllerAccessGroupsControllerLevelName { get; set; } = string.Empty;
    }
}

