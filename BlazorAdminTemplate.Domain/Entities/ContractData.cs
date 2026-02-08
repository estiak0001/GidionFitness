using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class ContractData
    {
        [JsonPropertyName("memberContractDataGUID")]
        public string MemberContractDataGUID { get; set; } = string.Empty;
        [JsonPropertyName("memberContractData")]
        public string MemberContractData { get; set; } = string.Empty;
        [JsonPropertyName("memberContractDate")]
        public DateTime MemberContractDate { get; set; }
    }
}
