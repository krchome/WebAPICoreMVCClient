using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Address { get; set; }
        //[Required]
        public string Telephone { get; set; }
       // [Required]
       // [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string Email { get; set; }
    }
}
