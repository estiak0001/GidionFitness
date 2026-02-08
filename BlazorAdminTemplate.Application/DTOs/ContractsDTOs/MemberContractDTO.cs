using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.ContractsDTOs
{
    public class MemberContractDTO
    {
        public string MemberContractDataSignageDate { get; set; } = string.Empty;
        public string MemberContractPDFFile { get; set; } = string.Empty;
        public string PdfDownloadUrl { get; set; } = string.Empty;

    }
}
