using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class MemberInfoNotification
    {
        public int MemberNotificationType { get; set; }
        public string MemberNotificationDateCreated { get; set; } = string.Empty;
        public string NotificationTypeDescription { get; set; } = string.Empty;
    }
}
