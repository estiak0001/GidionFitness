using BlazorAdminTemplate.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IMemberCardService
    {
        public Task<MemberPaymentCardInfoResponseDTO> GetPaymentCardInfoAsync();
        public Task<bool> DeletePaymentCardAsync(string cardGuid);

        public Task<MemberPaymentCreateResponseDTO> AddNewPaymentCardAsync();

    }
}
