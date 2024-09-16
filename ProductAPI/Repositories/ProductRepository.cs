using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _context;

    public ProductRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)

    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.SKU = product.SKU;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Category = product.Category;
            existingProduct.DateAdded = product.DateAdded;

            _context.Products.Update(existingProduct);
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
