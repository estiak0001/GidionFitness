using BlazorAdminTemplate.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IMemberPaymentService
    {
        Task<MemberPaymentListReponseDTO> GetMemberPaymentsAsync(int page);
        
    }
}
