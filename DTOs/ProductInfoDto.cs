namespace backend.DTOs
{
    public class ProductInfoDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Barcode {  get; set; }
        public string UOM {  get; set; }

        public string UOMQty {  get; set; }
        public decimal UnitCost {  get; set; }
    }
}
