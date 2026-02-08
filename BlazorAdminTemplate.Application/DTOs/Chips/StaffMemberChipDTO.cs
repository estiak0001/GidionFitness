using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Chips
{
    public class StaffMemberChipDTO
    {
    }

    public class StaffGetMemberChipDTO
    {
        public int? Page = 1;
        public int? PageSize = 15;
        public string? Search = string.Empty;
        public string? MemberGuid = string.Empty;
    }

    public class StaffGetMemberChipResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public string MemberGUID { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; }
        public List<MemberChip> MemberChips { get; set; } = new List<MemberChip>();
    }

    public class StaffMemberChipCreateDTO
    {
        public string MemberGUID { get; set; } = string.Empty;
        public string MemberChipData { get; set; } = string.Empty;
        public string MemberChipComment { get; set; } = string.Empty;
        public string MemberChipType { get; set; } = string.Empty;
    }

    public class StaffMemberChipEditDTO
    {
        public string MemberChipGUID { get; set; } = string.Empty;
        public string MemberChipData { get; set; } = string.Empty;
        public string MemberChipComment { get; set; } = string.Empty;
        public string MemberChipType { get; set; } = string.Empty;
    }

    public class StaffMemberChipActionResponseDTO 
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class StaffMemberChipDeleteResponseDTO
    {
        public string Message { get; set; } = string.Empty;
    }
}
