using System;
using System.Collections.Generic;

namespace PROGP2.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public DateOnly ProductionDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
