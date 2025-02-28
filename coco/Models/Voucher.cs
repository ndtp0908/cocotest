using System;
using System.Collections.Generic;

namespace coco.Models;

public partial class Voucher
{
    public string VoucherId { get; set; } = null!;

    public string VoucherCode { get; set; } = null!;

    public decimal VoucherDiscount { get; set; }

    public string VoucherStatus { get; set; } = null!;
}
