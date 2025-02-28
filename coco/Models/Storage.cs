using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class Storage
{
    public string ItemId { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public decimal ItemPrice { get; set; }

    public int ItemAmount { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
