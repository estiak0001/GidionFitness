using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.NewsDTO
{
    public class StaffEditNewsRequestDTO
    {
        public string MemberNewsGUID { get; set; } = string.Empty;
        public string MemberNewsSubject { get; set; } = string.Empty;
        public string MemberNewsText { get; set; } = string.Empty;
        public DateTime MemberNewsDate { get; set; }
    }
}
