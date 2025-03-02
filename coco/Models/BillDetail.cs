using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class BillDetail
{
    public int DetailId { get; set; }

    public string BillId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public int ItemCount { get; set; }

    public decimal Price { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Storage Item { get; set; } = null!;
}
