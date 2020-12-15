using System.ComponentModel.DataAnnotations;

namespace WebAPIConsume.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please put the Customer Id for this order")]
        public int CustomerId { get; set; }
        [Required (ErrorMessage ="Please put a description")]
        public string Description { get; set; }
        [Required (ErrorMessage = "Please put the order cost as a decimal")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal OrderCost { get; set; }
       // public Customer customer { get; set; }

    }
}
