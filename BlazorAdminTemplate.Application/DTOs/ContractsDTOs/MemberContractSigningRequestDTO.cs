using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Contracts
{
    public class MemberContractSigningRequestDTO
    {
        public string MemberContractDataGUID { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
    }
}
