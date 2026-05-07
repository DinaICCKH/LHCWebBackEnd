using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? RecipientId { get; set; }

    public string? RecipientType { get; set; }

    public int? SenderId { get; set; }

    public string? SenderType { get; set; }

    public string? Message { get; set; }

    public string? Type { get; set; }

    public string? RelatedEntityType { get; set; }

    public int? RelatedEntityId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsViewed { get; set; }

    public bool? IsGlobal { get; set; }
}
