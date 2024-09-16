namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public decimal StockQuantity { get; set; }
        public DateTime DateAdded { get; set; }

    }
}