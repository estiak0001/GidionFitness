using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Configuration
{
    public class ApiConfiguration
    {
        public string BaseUrl { get; set; } = string.Empty;
        public int Timeout { get; set; } = 30;
    }
}
