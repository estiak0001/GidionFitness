using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class FilterChangedEventArgs
    {
        public DateTime? SelectedDate { get; set; }
        public List<string> SelectedClassTypeGuids { get; set; } = new();
        public List<string> SelectedOrganizationGuids { get; set; } = new();
        public bool HasDateFilter { get; set; }
        public bool HasTreeViewFilters { get; set; }
        public bool HasActiveFilters => HasDateFilter || HasTreeViewFilters;
    }
}
