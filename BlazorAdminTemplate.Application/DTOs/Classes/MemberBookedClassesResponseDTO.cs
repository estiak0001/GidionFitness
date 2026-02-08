using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class MemberBookedClassesResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public string AppliedFilter { get; set; } = string.Empty;
        public List<BookedClasses> Classes { get; set; } = new List<BookedClasses>();
    }
}
