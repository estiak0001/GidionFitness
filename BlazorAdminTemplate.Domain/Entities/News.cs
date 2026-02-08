using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class News
    {
        public string MemberNewsGUID { get; set; } = string.Empty;
        public string MemberNewsSubject { get; set; } = string.Empty;
        public DateTime MemberNewsDate { get; set; }
    }
}
