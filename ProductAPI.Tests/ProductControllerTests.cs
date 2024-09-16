using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

public class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<IProductService> _mockService;

    public ProductsControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductsController(_mockService.Object);
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 100 },
            new Product { Id = 2, Name = "Product2", Price = 200 }
        };
        _mockService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equal(2, returnedProducts.Count());
    }

    [Fact]
    public async Task GetProduct_ReturnsOkResult_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1", Price = 100 };
        _mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _controller.GetProduct(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal("Product1", returnedProduct.Name);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProduct(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostProduct_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var product = new Product { Id = 3, Name = "Product3", Price = 300 };
        _mockService.Setup(s => s.AddProductAsync(product)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PostProduct(product);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedProduct = Assert.IsType<Product>(createdAtActionResult.Value);
        Assert.Equal("Product3", returnedProduct.Name);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1", Price = 100 };
        _mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _controller.DeleteProduct(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((Product)null);

        // Act
        var result = await _controller.DeleteProduct(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
