using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class NonCustomer
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }
}
