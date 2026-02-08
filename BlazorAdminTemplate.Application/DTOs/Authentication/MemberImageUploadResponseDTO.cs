using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class MemberImageUploadResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string MemberGuid { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public int FileSize { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
