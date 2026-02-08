using BlazorAdminTemplate.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<MemberPaymentInfoResponseDTO> MemberPaymentInfoAsync();

        Task<MemberPaymentCreateResponseDTO> MemberCreatePaymentAsync(MemberPaymentCreateRequestDTO request);
        
    }
}
