namespace Wholesaler.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<Product>? Products { get; set; }
    }
}
