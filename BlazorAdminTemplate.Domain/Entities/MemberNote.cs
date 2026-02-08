using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class MemberNote
    {
        public string MemberNotesGuid { get; set; } = string.Empty;
        public string MemberGuid { get; set; } = string.Empty;
        public DateTime MemberNotesDate { get; set; }
        public string MemberNotesText { get; set; } = string.Empty;
        public int MemberNotesVissible { get; set; }
        public string MemberName { get; set; } = string.Empty;
    }
}
