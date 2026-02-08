using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs
{
    public class ResponseDTO<T>
    {
        public string Message { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public string Search { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? Extra { get; set; }

        public T? Data { get; private set; }

        // Call this after deserialization to populate Data
        public void Initialize()
        {
            if (Data != null) return;
            if (Extra == null || Extra.Count == 0) return;

            var first = Extra.Values.First();
            Data = JsonSerializer.Deserialize<T>(first);
        }


        //var dto = await http.GetFromJsonAsync<GenericDto<T>>(url);
    }
}
