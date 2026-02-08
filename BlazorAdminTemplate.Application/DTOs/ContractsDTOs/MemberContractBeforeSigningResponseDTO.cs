using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Contracts
{
    public class MemberContractBeforeSigningResponseDTO
    {
        public bool HasUnsignedContract { get; set; }
        public string Message { get; set; } = string.Empty;
        public ContractData ContractData { get; set; } = new ContractData();
    }
}
