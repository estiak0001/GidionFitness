using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;

namespace BlazorAdminTemplate.Application.DTOs.Access
{
    public class StaffAccessControllerGroupDTO
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? Search { get; set; }
    }

    public class StaffAccessControllerGroupResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public AccessControllerGroup Data { get; set; } = new AccessControllerGroup();
    }

    public class StaffAccessControllerGroupListResponseDTO
    {
        public string message { get; set; } = string.Empty;
        public string search { get; set; } = string.Empty;
        public PaginationResponseDTO pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; }
        public List<AccessControllerGroup> AccessControllerGroups { get; set; } = new List<AccessControllerGroup>();
    }


    public class StaffAccessControllerGroupAddDTO
    {
        public string AccessControllerAccessGroupsName {get; set; } = string.Empty;
        public string AccessControllerAccessGroupsDescription {get; set; } = string.Empty;
        public int AccessControllerAccessGroupsControllerLevel {get; set; }
    }

    public class StaffAccessControllerGroupAddResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public AccessControllerGroup Data{ get; set; } = new AccessControllerGroup();
    }

    public class StaffAccessControllerGroupEditDTO
    {
        public string AccessControllerAccessGroupsGUID {get; set; } = string.Empty;
        public string AccessControllerAccessGroupsName { get;set; } = string.Empty;
        public string AccessControllerAccessGroupsDescription {get; set; } = string.Empty;
        public int AccessControllerAccessGroupsControllerLevel { get; set; }
    }
    public class StaffAccessControllerGroupEditResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public AccessControllerGroup Data{ get; set; } = new AccessControllerGroup();
    }

    public class StaffAccessControllerGroupDeleteResponseDTO
    {
        public string Message { get; set; } = string.Empty;
    }

    public class AccessControllerLevelsResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<AccessLevel> ControllerLevels { get; set; } = new List<AccessLevel>();
    }
}
