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
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsInvoiced { get; set; } = true;
    }
}
