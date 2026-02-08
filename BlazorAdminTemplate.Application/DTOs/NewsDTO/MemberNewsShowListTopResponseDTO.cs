using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.NewsDTO
{
    public class MemberNewsShowListTopResponseDTO
    {
        public int Count { get; set; }
        public List<News> MemberNews { get; set; } = new List<News>();
    }
}
