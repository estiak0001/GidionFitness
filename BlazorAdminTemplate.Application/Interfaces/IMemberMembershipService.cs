using BlazorAdminTemplate.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdminTemplate.Application.DTOs.Memberships;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IMemberMembershipService
    {
        public Task<MemberPaymentMembershipInfoResponseDTO> GetMembershipInfo();
        public Task<MemberCancelMembershipCheckResponseDTO> GetMemberMembershipCancelPolicy();
        public Task<MemberCancelMembershipResponseDTO> MemberMembershipCancel(MemberCancelMembershipDTO memberCancelMembershipDto);

    }
}
