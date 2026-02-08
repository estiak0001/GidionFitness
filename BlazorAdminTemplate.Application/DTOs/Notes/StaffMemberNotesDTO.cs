using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Notes
{
    public class StaffMemberNotesDTO
    {
    }

    public class StaffMemberNotesRequestDTO
    {
        public string? MemberGuid { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? Search { get; set; }
    }

    public class StaffMemberNotesResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public string MemberGuid { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; }
        public List<MemberNote> Notes { get; set; } = new List<MemberNote>();
    }

    public class StaffMemberNoteResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public MemberNote Note { get; set; } = new MemberNote();
    }

    public class StaffAddMemberNoteDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
        public string MemberNotesText { get; set; } = string.Empty;
        public bool MemberNoteVissible { get; set; }
    }

    public class StaffEditMemberNoteDTO
    {
        public string MemberNotesGuid { get; set; } = string.Empty;
        public string MemberNotesText { get; set; } = string.Empty;
        public bool MemberNoteVissible { get; set; }
    }

    public class StaffMemberNoteActionsResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
