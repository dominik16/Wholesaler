using Wholesaler.Models;

namespace Wholesaler.DataTransferObject
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public double Price { get; set; }

        public int StorageId { get; set; }
    }
}
