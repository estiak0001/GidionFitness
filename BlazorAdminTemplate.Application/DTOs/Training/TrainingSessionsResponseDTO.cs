using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Training
{
    public class TrainingSessionsResponseDTO
    {
        public string Team { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ArrivedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
