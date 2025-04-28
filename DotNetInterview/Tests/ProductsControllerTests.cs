using Microsoft.AspNetCore.Mvc;
using Moq;

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _mockService = new();
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _controller = new ProductsController(_mockService.Object);
    }

    [Fact]
    public void Get_ReturnsListOfProducts()
    {
        _mockService.Setup(service => service.GetAll()).Returns(new List<ProductDto>
        {
            new ProductDto { Name = "Celular", Price = 299.99m }
        });

        var result = _controller.Get().Result as OkObjectResult;
        var products = result.Value as List<ProductDto>;

        Assert.NotNull(products);
        Assert.Single(products);
    }

    [Fact]
    public void GetById_ReturnsProduct_WhenExists()
    {
        var productDto = new ProductDto { Name = "Computador", Price = 999.99m };
        _mockService.Setup(service => service.GetById(1)).Returns(productDto);

        var result = _controller.Get(1).Result as OkObjectResult;
        var product = result.Value as ProductDto;

        Assert.NotNull(product);
        Assert.Equal("Computador", product.Name);
        Assert.Equal(999.99m, product.Price);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockService.Setup(service => service.GetById(99)).Returns((ProductDto)null);

        var result = _controller.Get(99);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void AddProduct_ReturnsCreatedResponse()
    {
        var productDto = new CreateProductDto { Name = "Bicicleta", Price = 199.99m };

        var result = _controller.Add(productDto) as CreatedAtActionResult;

        Assert.NotNull(result);
        Assert.Equal(nameof(_controller.Get), result.ActionName);
    }

    [Fact]
    public void DeleteProduct_ReturnsNoContent_WhenProductExists()
    {
        _mockService.Setup(service => service.Delete(1)).Returns(true);

        var result = _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockService.Setup(service => service.Delete(99)).Returns(false);

        var result = _controller.Delete(99);

        Assert.IsType<NotFoundResult>(result);
    }
}