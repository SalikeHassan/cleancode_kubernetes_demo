using System;

namespace ECommerce.Domain.Entities;
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // Navigation property
    public ICollection<Order> Orders { get; set; } 
}
