using ProductAPI.Models;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddProductAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateProductAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
        await _productRepository.SaveChangesAsync();
    }
}
