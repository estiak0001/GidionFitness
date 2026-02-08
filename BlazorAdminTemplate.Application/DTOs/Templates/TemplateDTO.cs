using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Templates
{
    public class TemplateDTO
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? Search { get; set; }
    }

    public class TemplateListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; }
        public List<MailTemplate> Templates { get; set; } = new List<MailTemplate>();
    }

    public class TemplateResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public MailTemplate Template { get; set; } = new MailTemplate();
    }

    public class TemplatePlaceholderListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<MailTemplatePlaceholder> TemplatesPlaceholders { get; set; } = new List<MailTemplatePlaceholder>();
    }

    public class TemplateActionResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class TemplateEditDTO
    {
        public string MailTemplateGUID { get; set; } = string.Empty;
        public int MailTemplateType { get; set; }
        public string MailTemplateSubject { get; set; } = string.Empty;
        public string MailTemplateFromEmail { get; set; } = string.Empty;
        public string MailTemplateFromName { get; set; } = string.Empty;
        public string MailTemplateMessage { get; set; } = string.Empty;
        public string MailTemplateHelpText { get; set; } = string.Empty;
    }
}
