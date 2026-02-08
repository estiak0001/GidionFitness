using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CancelMembershipReason
{
    [Display(Name = "Member Request")]
    MemberRequest = 1,

    [Display(Name = "Non-Payment")]
    NonPayment = 2,

    [Display(Name = "Moved Away")]
    MovedAway = 3,

    [Display(Name = "No Longer Needed")]
    NoLongerNeeded = 4,

    [Display(Name = "Switched to Another Gym")]
    SwitchedProvider = 5,

    [Display(Name = "Financial Reasons")]
    FinancialReasons = 6,

    [Display(Name = "Other")]
    Other = 99
}

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayName = enumValue.GetType()
            .GetMember(enumValue.ToString())[0]
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault()?.Name;

        return displayName ?? enumValue.ToString();
    }
}
