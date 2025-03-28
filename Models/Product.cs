﻿using System;
using System.Collections.Generic;

namespace E_handelWEBapplication.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
