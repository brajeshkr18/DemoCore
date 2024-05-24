namespace SampleAPI.Requests
{
    public class OrderRequestList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsInvoiced { get; set; } = true;
    }
}
