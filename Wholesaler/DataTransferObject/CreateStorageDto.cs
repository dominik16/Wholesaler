namespace Wholesaler.DataTransferObject
{
    public class CreateStorageDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
