using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class AccessControllerGroup
    {
      public string AccessControllerAccessGroupsGUID { get; set; } = string.Empty;
      public string AccessControllerAccessGroupsName { get; set; } = string.Empty;
      public string AccessControllerAccessGroupsDescription { get; set; } = string.Empty;
      public int AccessControllerAccessGroupsControllerLevel { get; set; }
      public string AccessControllerAccessGroupsControllerLevelName { get; set; } = string.Empty;

    }
}
