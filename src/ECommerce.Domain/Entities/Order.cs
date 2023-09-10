using System;

namespace ECommerce.Domain.Entities;
public class Order
{
    public int OrderId { get; set; }
    public DateTime DatePlaced { get; set; }
    public decimal TotalAmount { get; set; }

    // Foreign key for Customer
    public int CustomerId { get; set; }

    // Navigation property
    public Customer Customer { get; set; }
}