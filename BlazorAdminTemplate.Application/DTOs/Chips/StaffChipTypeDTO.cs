using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Chips
{
    public class StaffChipTypeDTO
    {
    }

    public class StaffGetChipTypeResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<ChipType> ChipTypes { get; set; } = new List<ChipType>();
    }
}
