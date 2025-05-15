using backend.DTOs;
using backend.Models;
using backend.Repository.InventoryMasterRepository;

namespace backend.Services.InventoryMasterServices
{
    public interface IInventoryItemMasterServices
    {
        Task<bool> AddProductAsync(ProductDto productDto);
    }

    public class InventoryItemMasterServices : IInventoryItemMasterServices
    {
        private readonly IInventoryItemMasterRepository _inventoryItemMasterRepository;

        public InventoryItemMasterServices(IInventoryItemMasterRepository inventoryItemMasterRepository)
        {
            _inventoryItemMasterRepository = inventoryItemMasterRepository;
        }

        public async Task<bool> AddProductAsync(ProductDto productDto)
        {
            try
            {
                // Generate ProductID
                var newProductId = await _inventoryItemMasterRepository.GenerateNewProductIdAsync();

                // Convert names to IDs
                var catId = await _inventoryItemMasterRepository.GetCatIdByCatNameAsync(productDto.ProductMastDTO.CatName);
                var subCatId = await _inventoryItemMasterRepository.GetSubCatIdBySubCatNameAsync(productDto.ProductMastDTO.SubCatName);
                var uomData = await _inventoryItemMasterRepository.GetUOMByNameAsync(productDto.productUOMDTO.UOM);

                if (string.IsNullOrEmpty(catId) || string.IsNullOrEmpty(subCatId) || uomData == null)
                    return false;

                var vatRate = await _inventoryItemMasterRepository.GetVateRateByCategoryIdAsync(catId);

                // Set final values in DTO
                productDto.ProductMastDTO.ProductId = newProductId;
                productDto.productUOMDTO.ProductId = newProductId;
                productDto.productUOMDTO.Barcode = newProductId;

                productDto.ProductMastDTO.CatId = catId;
                productDto.ProductMastDTO.SubCatId = subCatId;
                productDto.ProductMastDTO.VatRate = vatRate;
                productDto.ProductMastDTO.UnitRate = productDto.productUOMDTO.UnitRate;
                productDto.ProductMastDTO.SaleRate = productDto.productUOMDTO.UnitSale;
                productDto.ProductMastDTO.WholeSalePcs = productDto.productUOMDTO.WS;

                // Create entities
                var productMaster = new ProductMaster
                {
                    ProductID = newProductId,
                    ProductName = productDto.ProductMastDTO.ProductName,
                    CatID = catId,
                    SubCatID = subCatId,
                    UnitRate = productDto.ProductMastDTO.UnitRate,
                    SaleRate = productDto.ProductMastDTO.SaleRate,
                    WholeSalePcs = productDto.ProductMastDTO.WholeSalePcs,
                    VatRate = vatRate,
                    EntryBy = "admin",
                    EntryDate = DateTime.Now
                };

                var productUOM = new ProductUOM
                {
                    ProductID = newProductId,
                    Barcode = newProductId,
                    UOM = uomData.UomDesc,
                    UOMQty = uomData.UomQty,
                    UnitRate = productDto.productUOMDTO.UnitRate,
                    UnitSale = productDto.productUOMDTO.UnitSale,
                    WS = productDto.productUOMDTO.WS,
                    DiscAmt = productDto.productUOMDTO.DiscAmt,
                    NetSale = productDto.productUOMDTO.UnitSale,
                    EntryBy = "admin",
                    EntryDate = DateTime.Now
                };

                await _inventoryItemMasterRepository.AddProductAsync(productMaster, productUOM);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

