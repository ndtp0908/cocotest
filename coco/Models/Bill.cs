using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class Bill
{
    public string BillId { get; set; } = null!;

    public string? UserId { get; set; }

    public string ItemId { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public decimal ItemPrice { get; set; }

    public int ItemCount { get; set; }

    public string? Discount { get; set; }

    public decimal? Total { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string EndAddress { get; set; } = null!;

    public string? Status { get; set; }

    public virtual Storage Item { get; set; } = null!;

    public virtual User? User { get; set; }
}
