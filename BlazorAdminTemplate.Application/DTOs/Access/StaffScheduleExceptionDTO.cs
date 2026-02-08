using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Access
{
    public class StaffScheduleExceptionDTO
    {
        public string AccessGroupGuid { get; set; } = string.Empty;
    }

    public class StaffScheduleExceptionResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public ScheduleExceptions Data { get; set; } = new ScheduleExceptions();
    }

    public class StaffScheduleExceptionListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string AccessGroupGuid { get; set; } = string.Empty;
        public string AccessGroupName { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<ScheduleExceptions> ScheduleExceptions { get; set; } = new List<ScheduleExceptions>();
    }

    public class StaffScheduleExceptionCreateDTO
    {
        public string AccessControllerAccessGroupsGUID { get; set; } = string.Empty;

        public string AccessControllerScheduleExceptionName { get; set; } = string.Empty;

        public string AccessControllerScheduleExceptionDescription { get; set; } = string.Empty;

        public DateTime AccessControllerScheduleExceptionFromDateTime { get; set; }

        public DateTime AccessControllerScheduleExceptionToDateTime { get; set; }

        public int AccessControllerScheduleExceptionAccessStatus { get; set; }

        public bool AccessControllerScheduleExceptionEnabled { get; set; }
    }
    public class StaffScheduleExceptionEditDTO
    {
        public string AccessControllerScheduleExceptionGUID { get; set; }

        public string AccessControllerAccessGroupsGUID { get; set; } = string.Empty;

        public string AccessControllerScheduleExceptionName { get; set; } = string.Empty;

        public string AccessControllerScheduleExceptionDescription { get; set; } = string.Empty;

        public DateTime AccessControllerScheduleExceptionFromDateTime { get; set; }

        public DateTime AccessControllerScheduleExceptionToDateTime { get; set; }

        public int AccessControllerScheduleExceptionAccessStatus { get; set; }

        public bool AccessControllerScheduleExceptionEnabled { get; set; }
    }

    public class StaffScheduleExceptionCreateResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public ScheduleExceptions Data { get; set; } = new ScheduleExceptions();
    }

    public class StaffScheduleExceptionEditResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public ScheduleExceptions Data { get; set; } = new ScheduleExceptions();
    }

    public class StaffScheduleExceptionDeleteResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
