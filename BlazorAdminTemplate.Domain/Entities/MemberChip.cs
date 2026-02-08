using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class MemberChip
    {
        public string MemberChipGUID { get; set; } = string.Empty;
        public string MemberGUID { get; set; } = string.Empty;
        public DateTime MemberChipCreated { get; set; }
        public string MemberChipData { get; set; } = string.Empty;
        public string MemberChipComment { get; set; } = string.Empty;
        public string MemberChipType { get; set; } = string.Empty;
        public string MemberChipTypeName { get; set; } = string.Empty;
    }
}
