namespace backend.DTOs
{
    public class ProductDto
    {
        public ProductMastDTO ProductMastDTO { get; set; }
        public ProductUOMDTO productUOMDTO { get; set; }
    }

    public class ProductMastDTO
    {
        public string ProductId {  get; set; }
        public string ProductName { get; set; }

        public string CatName {  get; set; }

        public string SubCatName {  get; set; }
        public string CatId {  get; set; }

        public string SubCatId {  get; set; }

        public decimal UnitRate { get; set; }

        public decimal SaleRate { get; set; }

        public decimal WholeSalePcs { get; set; }

        public decimal VatRate {  get; set; }
    }

    public class ProductUOMDTO
    {
        public string ProductId { get; set; }

        public string Barcode { get; set; }

        public string UOM { get; set; }

        public decimal UOMQty { get; set; }

        public decimal UnitRate { get; set; }

        public decimal UnitSale { get; set; }

        public decimal DiscAmt { get; set; }

        public decimal NetSale { get; set; }

        public decimal WS { get; set; }

    }

    
}
