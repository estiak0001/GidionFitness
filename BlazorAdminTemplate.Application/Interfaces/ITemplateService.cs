using BlazorAdminTemplate.Application.DTOs.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface ITemplateService
    {
        public Task<TemplateListResponseDTO> StaffGetMailTemplatesAsync(TemplateDTO query);
        public Task<TemplateResponseDTO> StaffGetMailTemplateAsync(string templateGUID);
        public Task<TemplatePlaceholderListResponseDTO> StaffGetMailTemplatePlaceholdersAsync();

        public Task<TemplateActionResponseDTO> StaffEditMailTemplateAsync(TemplateEditDTO template);
    }
}
