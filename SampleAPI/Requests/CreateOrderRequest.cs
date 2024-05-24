using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Requests
{
    public class CreateOrderRequest
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
    }
}
