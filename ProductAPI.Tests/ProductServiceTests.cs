using Moq;
using ProductAPI.Models;

public class ProductServiceTests
{
    private readonly ProductService _service;
    private readonly Mock<IProductRepository> _mockRepo;

    public ProductServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 100 },
            new Product { Id = 2, Name = "Product2", Price = 200 }
        };
        _mockRepo.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var result = await _service.GetAllProductsAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Product1", result.First().Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1", Price = 100 };
        _mockRepo.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _service.GetProductByIdAsync(1);

        // Assert
        Assert.Equal("Product1", result.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetProductByIdAsync(99)).ReturnsAsync((Product)null);

        // Act
        var result = await _service.GetProductByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddProduct_CallsRepositoryAddAndSave()
    {
        // Arrange
        var product = new Product { Id = 3, Name = "Product3", Price = 300 };

        // Act
        await _service.AddProductAsync(product);

        // Assert
        _mockRepo.Verify(r => r.AddProductAsync(product), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_CallsRepositoryDeleteAndSave()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1", Price = 100 };
        _mockRepo.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

        // Act
        await _service.DeleteProductAsync(1);

        // Assert
        _mockRepo.Verify(r => r.DeleteProductAsync(1), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
