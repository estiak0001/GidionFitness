using System.Runtime.InteropServices.JavaScript;

namespace BlazorAdminTemplate.Application.DTOs.Memberships;

public class MemberCancelMembershipCheckDTO
{
}

public class MemberCancelMembershipCheckResponseDTO
{
    public bool CanCancel { get; set; }
    public DateTime? EarliestCancellationDate { get; set; }
    public DateTime? MemberCreatedDate { get; set; }
    public int? BindingPeriodMonths { get; set; }
    public int? NoticePeriodMonths { get; set; }
    public bool? CanCancelInBindingPeriod { get; set; }
    public bool? CanCancelNow { get; set; }
    public string? Message { get; set; }
}