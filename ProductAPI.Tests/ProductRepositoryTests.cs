using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

public class ProductRepositoryTests
{
    private readonly DbContextOptions<ProductContext> _dbContextOptions;

    public ProductRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    private ProductContext GetDbContext()
    {
        return new ProductContext(_dbContextOptions);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsAllProducts()
    {
        // Arrange
        using var context = GetDbContext();
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 100 },
            new Product { Id = 2, Name = "Product2", Price = 200 }
        };
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context);

        // Act
        var result = await repository.GetAllProductsAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Product1", result.ToList()[0].Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct_WhenExists()
    {
        // Arrange
        using var context = GetDbContext();
        var product = new Product { Id = 1, Name = "Product1", Price = 100 };
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context);

        // Act
        var result = await repository.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Product1", result.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsNull_WhenNotExists()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new ProductRepository(context);

        // Act
        var result = await repository.GetProductByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddProduct_AddsProductToDatabase()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new ProductRepository(context);
        var product = new Product { Id = 3, Name = "Product3", Price = 300 };

        // Act
        await repository.AddProductAsync(product);
        await repository.SaveChangesAsync();

        // Assert
        var addedProduct = await context.Products.FindAsync(3);
        Assert.NotNull(addedProduct);
        Assert.Equal("Product3", addedProduct.Name);
    }

    [Fact]
    public async Task DeleteProduct_RemovesProductFromDatabase_WhenExists()
    {
        // Arrange
        using var context = GetDbContext();
        var product = new Product { Id = 1, Name = "Product1", Price = 100 };
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        var repository = new ProductRepository(context);

        // Act
        await repository.DeleteProductAsync(1);
        await repository.SaveChangesAsync();

        // Assert
        var deletedProduct = await context.Products.FindAsync(1);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task DeleteProduct_DoesNothing_WhenProductDoesNotExist()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new ProductRepository(context);

        // Act
        await repository.DeleteProductAsync(99);
        await repository.SaveChangesAsync();

        // Assert
        var productCount = await context.Products.CountAsync();
        Assert.Equal(0, productCount);  // No product should be deleted
    }
}
