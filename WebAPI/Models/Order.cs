
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        //[Required]
        public int CustomerId { get; set; }
        //[Required]
        public string Description { get; set; }
       // [Required]
        public decimal OrderCost { get; set; }
       // public Customer customer { get; set; }
    }
}
