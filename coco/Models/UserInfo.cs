using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class UserInfo
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public string Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
