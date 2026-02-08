using BlazorAdminTemplate.Application.DTOs.Organisations;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IOrganisationService
    {
        Task<MainOrganisationResponseDTO> GetMainOrganisationAsync(string mainOrganisationGUID);
        Task<OrganisationDomain> GetOrganisationByDomainAsync(string domain);

        Task<SubOrganisationResponseDTO> GetSubOrganisationsAsync(string mainOrganisationGUID);

        string GetCurrentMainOrganisationGUID();

        Task<StaffChangeOrganisationResponseDTO> ChangeOrganisationAsync(StaffChangeOrganisationDTO request);
    }
}
