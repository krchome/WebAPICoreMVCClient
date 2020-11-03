using System.ComponentModel.DataAnnotations;

namespace WebAPIConsume.Models
{
    
        public class Customer
        {
            public int Id { get; set; }
            [Required(ErrorMessage ="Please put a Name")]
            public string Name { get; set; }
            [Required(ErrorMessage ="Please put address")]
            public string Address { get; set; }
            [Required(ErrorMessage ="Please put a valid telephone no.")]
            public string Telephone { get; set; }
            [Required(ErrorMessage ="Please put a valid email")]
            [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
            public string Email { get; set; }
        }
    
}
