using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IPagination
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        int TotalRecords { get; set; }
        int TotalPages { get; set; }
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }
    }
}
