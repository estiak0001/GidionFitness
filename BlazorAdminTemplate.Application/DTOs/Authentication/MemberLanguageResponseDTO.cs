using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class MemberLanguageResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<Language> languages { get; set; } = new List<Language>();
    }
}
