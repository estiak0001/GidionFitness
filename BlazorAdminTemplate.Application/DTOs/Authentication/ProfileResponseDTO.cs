using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class ProfileResponseDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
        public string OrganisationMainGuid { get; set; } = string.Empty;
        public string OrganisationSubGuid { get; set; } = string.Empty;
        public string MemberPaymentGroupGuid { get; set; } = string.Empty;
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty;
        public string MemberCreated { get; set; } = string.Empty;
        public string MemberAddress { get; set; } = string.Empty;
        public string MemberCity { get; set; } = string.Empty;
        public int MemberZipCode { get; set; }
        public string MemberBirthdate { get; set; } = string.Empty;
        public int MemberSex { get; set; }
        public string MemberCPR { get; set; } = string.Empty;
        public string MemberPhonePrivate { get; set; } = string.Empty;
        public string MemberPhoneWork { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public byte MemberActive { get; set; }
        public string MemberLastSeen { get; set; } = string.Empty;
        public string MemberComment { get; set; } = string.Empty;
        public string MemberJobTitle { get; set; } = string.Empty;
        public string MemberTypeName { get; set; } = string.Empty;
        public int MemberType { get; set; }
        public int MemberPaymentCardStatus { get; set; }
        public bool IsActive { get; set; }
        public List<MemberInfoNotification> Notifications { get; set; } = new List<MemberInfoNotification>();
    }
}
