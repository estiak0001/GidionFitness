using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Contracts
{
    public class MemberContractSigningResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string MemberContractDataGUID { get; set; } = string.Empty;
        public string SignatureFile { get; set; } = string.Empty;
        public string PdfFile { get; set; } = string.Empty;
        public DateTime SignedDate { get; set; }
    }
}
