using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class MailTemplatePlaceholder
    {
        public string MailTemplatePlaceholdersName { get; set; } = string.Empty;
        public string MailTemplatePlaceholdersValue { get; set; } = string.Empty;
        public string MailTemplatePlaceholdersDescription { get; set; } = string.Empty;
    }
}
