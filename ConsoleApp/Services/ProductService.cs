using ConsoleApp.Entities;
using ConsoleApp.Repositories;

namespace ConsoleApp.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductEntity> CreateProductAsync(string productName, string description, decimal price, int stockStatus)
    {
        var productEntity = new ProductEntity
        {
            ProductName = productName,
            Description = description,
            Price = price,
            StockStatus = stockStatus
        };

        await _productRepository.CreateAsync(productEntity);

        return productEntity;
    }

    public async Task<ProductEntity> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetAsync(x => x.Id == productId);
    }

    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
    {
        return await _productRepository.GetAsync();
    }

    public async Task<ProductEntity> UpdateProductAsync(int productId, string productName, string description, decimal price, int stockStatus)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == productId);
        if (productEntity == null)
        {
            throw new KeyNotFoundException("Produkt hittades inte med ID: " + productId);
        }

        productEntity.ProductName = productName;
        productEntity.Description = description;
        productEntity.Price = price;
        productEntity.StockStatus = stockStatus;

        await _productRepository.UpdateAsync(x => x.Id == productId, productEntity);

        return productEntity;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == productId);
        if (productEntity == null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(productEntity);
        return true;
    }
}
