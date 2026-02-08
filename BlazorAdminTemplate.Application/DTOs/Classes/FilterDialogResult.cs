using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class FilterDialogResult
    {
        public DateTime? SelectedDate { get; set; }
        public List<FilterItem> SelectedFilters { get; set; } = new List<FilterItem>();
    }
}
