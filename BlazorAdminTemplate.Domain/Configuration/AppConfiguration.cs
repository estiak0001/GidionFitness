using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Configuration
{
    public class AppConfiguration
    {
        public ApiConfiguration ApiConfiguration { get; set; } = new ApiConfiguration();
        public FeaturesConfiguration Features { get; set; } = new FeaturesConfiguration();
    }
}
