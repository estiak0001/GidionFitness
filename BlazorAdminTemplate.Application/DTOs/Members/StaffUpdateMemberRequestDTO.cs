using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Members
{
    public class StaffUpdateMemberRequestDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
        public string OrganisationMainGuid { get; set; } = string.Empty;
        public string OrganisationSubGuid { get; set; } = string.Empty;
        public string MemberPaymentGroupGuid { get; set; } = string.Empty;
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty;
        public string MemberAddress { get; set; } = string.Empty;
        public string MemberCity { get; set; } = string.Empty;
        public int MemberZipCode { get; set; }
        public DateTime? MemberBirthday { get; set; }
        public byte? MemberSex { get; set; }
        public string MemberCPR { get; set; } = string.Empty;
        public string MemberPhonePrivate { get; set; } = string.Empty;
        public string MemberPhoneWork { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public string MemberJobTitle { get; set; } = string.Empty;
        public string MemberLanguageGUID { get; set; } = string.Empty;
        public byte? MemberHealthInsurance { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
