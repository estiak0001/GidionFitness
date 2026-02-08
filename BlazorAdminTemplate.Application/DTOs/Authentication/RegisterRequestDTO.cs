using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class RegisterRequestDTO
    {
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty;
        public string OrganisationMainGUID { get; set; } = string.Empty;
        public string OrganisationSubGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public string MemberPassword { get; set; } = string.Empty;
        public string MemberAddress { get; set; } = string.Empty;
        public string MemberCity { get; set; } = string.Empty;
        public int MemberZipCode { get; set; } = 0;
        public DateTime? Birthday { get; set; }
        public int MemberSex { get; set ; } = 0; 
        public string MemberPhonePrivate { get; set; } = string.Empty;
        public string MemberPhoneWork { get; set; } = string.Empty;
        public string MemberJobTitle { get; set; } = string.Empty;
        public string MemberComment { get; set; } = string.Empty;
        public string MemberCPR { get; set; } = string.Empty;
        public string MemberLanguageGUID { get; set; } = string.Empty;
        public byte MemberHealthInsurance { get; set; } = 0;
    }
}
