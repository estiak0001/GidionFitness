using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class Member
    {
        public string MemberGUID { get; set; } = string.Empty;
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty;
        public string MemberAddress { get; set; } = string.Empty;
        public string MemberCity { get; set; } = string.Empty;
        public int MemberZipCode { get; set; }
        public DateTime? MemberBirthday { get; set; }
        public byte? MemberSex { get; set; }
        public string MemberPhonePrivate { get; set; } = string.Empty;
        public string MemberPhoneWork { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public int MemberActive { get; set; }
        public DateTime? MemberLastSeen { get; set; }
        public byte? MemberTrial { get; set; }
        public string MemberImage { get; set; } = string.Empty;
        public DateTime? MemberLastPayment { get; set; }
        public int MemberType { get; set; }
        public byte? MemberPause { get; set; }
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupName { get; set; } = string.Empty;
    }
}
