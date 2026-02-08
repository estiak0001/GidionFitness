using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Access
{
    public class StaffScheduleDayConfigDTO
    {
        public string AccessControllerScheduleDayConfigGUID { get; set; } = string.Empty;
    }

    public class StaffScheduleDayConfigResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string AccessControllerAccessGroupsGUID { get; set; } = string.Empty;
        public string AccessControllerAccessGroupsName { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<ScheduleDayConfig> ScheduleDayConfigs { get; set; } = new List<ScheduleDayConfig>();
    }

    public class StaffScheduleDayConfigToggleDTO
    {
        public string AccessControllerScheduleDayConfigGUID { get; set; } = string.Empty;
        public bool AccessControllerScheduleDayConfigEnabled { get; set; }
    }

    public class StaffScheduleDayConfigToggleResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
