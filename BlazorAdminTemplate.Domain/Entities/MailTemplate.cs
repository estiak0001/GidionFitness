using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class MailTemplate
    {
        public string MailTemplateGUID { get; set; } = string.Empty;
        public int MailTemplateType { get; set; }
        public string MailTemplateSubject { get; set; } = string.Empty;
        public string MailTemplateFromEmail { get; set; } = string.Empty;
        public string MailTemplateFromName { get; set; } = string.Empty;
        public string MailTemplateMessage { get; set; } = string.Empty;
        public string MailTemplateName { get; set; } = string.Empty;
        public string MailTemplateHelpText { get; set; } = string.Empty;
    }
}
