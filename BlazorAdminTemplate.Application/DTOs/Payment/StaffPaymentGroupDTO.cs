using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class StaffPaymentGroupDTO
    {
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupName { get; set; } = string.Empty;
        public string MemberPaymentGroupDescription { get; set; } = string.Empty;
        public DateTime? MemberPaymentValidFrom { get; set; }
        public DateTime? MemberPaymentValidTo { get; set; }
        public decimal MemberPaymentPrice { get; set; }
        public int MemberPaymentPriceVAT { get; set; }
        public int MemberPaymentEnable { get; set; }
        public int MemberPaymentDefault { get; set; }
        public string PaymentProviderDataGUID { get; set; } = string.Empty;
        public decimal? MemberPaymentPausePrice { get; set; }
        public string MemberGroupDefaultGroupsGUID { get; set; } = string.Empty;
    }

    public class StaffPaymentGroupCreateDTO
    {
        public string MemberPaymentGroupName { get; set; } = string.Empty;
        public string MemberPaymentGroupDescription { get; set; } = string.Empty;
        public DateTime? MemberPaymentValidFrom { get; set; }
        public DateTime? MemberPaymentValidTo { get; set; }
        public decimal MemberPaymentPrice { get; set; }
        public bool MemberPaymentPriceVAT { get; set; }
        public bool MemberPaymentEnable { get; set; }
        public bool MemberPaymentDefault { get; set; }
        public string PaymentProviderDataGUID { get; set; } = string.Empty;
        public decimal? MemberPaymentPausePrice { get; set; }
        public string MemberGroupDefaultGroupsGUID { get; set; } = string.Empty;
    }

    public class StaffPaymentGroupUpdateDTO
    {
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupName { get; set; } = string.Empty;
        public string MemberPaymentGroupDescription { get; set; } = string.Empty;
        public DateTime? MemberPaymentValidFrom { get; set; }
        public DateTime? MemberPaymentValidTo { get; set; }
        public decimal MemberPaymentPrice { get; set; }
        public bool MemberPaymentPriceVAT { get; set; }
        public bool MemberPaymentEnable { get; set; }
        public bool MemberPaymentDefault { get; set; }
        public string PaymentProviderDataGUID { get; set; } = string.Empty;
        public decimal? MemberPaymentPausePrice { get; set; }
        public string MemberGroupDefaultGroupsGUID { get; set; } = string.Empty;
    }

    public class StaffPaymentGroupListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public List<StaffPaymentGroupDTO> PaymentGroups { get; set; } = new();
    }

    public class StaffPaymentGroupSingleResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public StaffPaymentGroupDTO? PaymentGroup { get; set; }
    }

    public class StaffPaymentGroupResponseDTO
    {
        public string Message { get; set; } = string.Empty;
    }

    public class PaymentProviderDTO
    {
        public string PaymentProviderDataGUID { get; set; } = string.Empty;
        public string PaymentProviderName { get; set; } = string.Empty;
    }

    public class PaymentProviderListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public List<PaymentProviderDTO> PaymentProviders { get; set; } = new();
    }

    public class MemberGroupDefaultDTO
    {
        public string MemberGroupDefaultGroupsGUID { get; set; } = string.Empty;
        public string MemberGroupDefaultGroupsName { get; set; } = string.Empty;
    }

    public class MemberGroupDefaultListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public List<MemberGroupDefaultDTO> MemberGroupDefaultGroups { get; set; } = new();
    }
}
