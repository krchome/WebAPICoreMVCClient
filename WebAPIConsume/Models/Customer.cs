using System.ComponentModel.DataAnnotations;

namespace WebAPIConsume.Models
{
        public class Customer
        {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please put a Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please put address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please put a valid 10 digit telephone no.")]
        [RegularExpression(@"^[0-9]{10}$")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Please put a valid email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string Email { get; set; }
    }
    
}
