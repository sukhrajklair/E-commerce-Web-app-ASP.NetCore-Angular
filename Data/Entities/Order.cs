using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    [Required]
    public string OrderNumber { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public User User { get; set; }
  }
}
