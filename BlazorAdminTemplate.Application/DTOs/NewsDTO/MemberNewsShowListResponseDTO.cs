using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.NewsDTO
{
    public class MemberNewsShowListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; }
        public List<News> News { get; set; } = new List<News>();
    }
}
