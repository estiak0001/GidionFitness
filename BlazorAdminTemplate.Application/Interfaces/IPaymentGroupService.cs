using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IPaymentGroupService
    {
        Task<PaymentGroupResponseDTO> GetPaymentGroupAsync(string orgMainGuid, string orgSubGuid);

        // Staff Payment Group Management
        Task<StaffPaymentGroupListResponseDTO> GetPaymentGroupsAsync(int page = 1, int pageSize = 15, string search = "");
        Task<StaffPaymentGroupSingleResponseDTO> GetPaymentGroupByIdAsync(string groupGuid);
        Task<StaffPaymentGroupResponseDTO> CreatePaymentGroupAsync(StaffPaymentGroupCreateDTO request);
        Task<StaffPaymentGroupResponseDTO> UpdatePaymentGroupAsync(StaffPaymentGroupUpdateDTO request);
        Task<StaffPaymentGroupResponseDTO> DeletePaymentGroupAsync(string groupGuid);

        // Helper endpoints
        Task<PaymentProviderListResponseDTO> GetPaymentProvidersAsync();
        Task<MemberGroupDefaultListResponseDTO> GetDefaultGroupsAsync();
    }
}
