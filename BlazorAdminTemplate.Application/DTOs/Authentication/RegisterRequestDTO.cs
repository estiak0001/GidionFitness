using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class RegisterRequestDTO
    {
        [JsonPropertyName("memberFirstName")]
        public string MemberFirstName { get; set; } = string.Empty;
        
        [JsonPropertyName("memberLastName")]
        public string MemberLastName { get; set; } = string.Empty;
        
        [JsonPropertyName("organisationMainGUID")]
        public string OrganisationMainGUID { get; set; } = string.Empty;
        
        [JsonPropertyName("organisationSubGUID")]
        public string OrganisationSubGUID { get; set; } = string.Empty;
        
        [JsonPropertyName("memberPaymentGroupGUID")]
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        
        [JsonPropertyName("memberEmail")]
        public string MemberEmail { get; set; } = string.Empty;
        
        [JsonPropertyName("memberPassword")]
        public string MemberPassword { get; set; } = string.Empty;
        
        [JsonPropertyName("memberType")]
        public int MemberType { get; set; } = 1; // 1 = Member, 2 = Staff
        
        [JsonPropertyName("memberAddress")]
        public string MemberAddress { get; set; } = string.Empty;
        
        [JsonPropertyName("memberCity")]
        public string MemberCity { get; set; } = string.Empty;
        
        [JsonPropertyName("memberZipCode")]
        public int MemberZipCode { get; set; } = 0;
        
        [JsonPropertyName("memberBirthday")]
        public DateTime? MemberBirthday { get; set; }
        
        [JsonPropertyName("memberSex")]
        public int MemberSex { get; set ; } = 0; 
        
        [JsonPropertyName("memberPhonePrivate")]
        public string MemberPhonePrivate { get; set; } = string.Empty;
        
        [JsonPropertyName("memberPhoneWork")]
        public string MemberPhoneWork { get; set; } = string.Empty;
        
        [JsonPropertyName("memberJobTitle")]
        public string MemberJobTitle { get; set; } = string.Empty;
        
        [JsonPropertyName("memberComment")]
        public string MemberComment { get; set; } = string.Empty;
        
        [JsonPropertyName("memberCPR")]
        public string MemberCPR { get; set; } = string.Empty;
        
        [JsonPropertyName("memberLanguageGUID")]
        public string MemberLanguageGUID { get; set; } = string.Empty;
        
        [JsonPropertyName("memberHealthInsurance")]
        public string MemberHealthInsurance { get; set; } = "0";
        
        [JsonPropertyName("memberNewsLetter")]
        public string MemberNewsLetter { get; set; } = "0";
    }
}
