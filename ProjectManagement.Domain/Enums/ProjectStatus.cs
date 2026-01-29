using System;
using System.Collections.Generic;

namespace ProjectManagement.Domain.Entities
{
    public enum ProjectStatus
    {
        NotStarted = 0,
        InProgress,
        Completed,
        OnHold
    }
}