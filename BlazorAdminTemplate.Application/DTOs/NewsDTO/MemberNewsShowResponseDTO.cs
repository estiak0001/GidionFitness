using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.NewsDTO
{
    public class MemberNewsShowResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public News News { get; set; } = new News();
        public string MemberNewsText { get; set; } = string.Empty;
    }
}
