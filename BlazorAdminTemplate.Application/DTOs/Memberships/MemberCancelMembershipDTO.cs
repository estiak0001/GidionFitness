namespace BlazorAdminTemplate.Application.DTOs.Memberships;

public class MemberCancelMembershipDTO
{
    public DateTime CancellationDate { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
}

public class MemberCancelMembershipResponseDTO
{
    public string Message { get; set; } = string.Empty;
    
    // Success properties
    public string? CancellationGuid { get; set; }
    public string? CancellationDate { get; set; }
    public bool? EmailSent { get; set; }
    
    // Error properties
    public DateTime? EarliestAllowedDate { get; set; }
}