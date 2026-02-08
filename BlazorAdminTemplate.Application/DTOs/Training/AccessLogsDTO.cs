using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Training
{
    public class AccessLogsDTO
    {
        public string MemberAccessLogTime { get; set; } = string.Empty;
        public int MemberAccessLogState { get; set; }
        public string AccessControllerName { get; set; } = string.Empty;
        public string OrganisationMainName { get; set; } = string.Empty;
        public string OrganisationSubName { get; set; } = string.Empty;
    }
}
