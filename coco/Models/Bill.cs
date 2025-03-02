using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class Bill
{
    public string BillId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Discount { get; set; }

    public decimal? Total { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string EndAddress { get; set; } = null!;

    public string? Status { get; set; }

    public DateOnly DayBought { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual User? User { get; set; }
}
