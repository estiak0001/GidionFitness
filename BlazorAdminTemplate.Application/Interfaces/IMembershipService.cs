using BlazorAdminTemplate.Application.DTOs.Memberships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IMembershipService
    {
        public Task<MembershipsResponseDTO> GetMembershipListAsync();

    }
}
