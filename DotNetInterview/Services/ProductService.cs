public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductDto> GetAll()
    {
        try
        {
            return _repository.GetAll()
                .Select(p => new ProductDto { Name = p.Name, Price = p.Price })
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while retrieve all products", ex);
        }
    }

    public ProductDto GetById(int id)
    {
        try
        {
            var product = _repository.GetById(id);
            return product == null ? null : new ProductDto { Name = product.Name, Price = product.Price };
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while retrieve the product", ex);
        }
    }

    public void Add(CreateProductDto productDto)
    {
        try
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };

            _repository.Add(product);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while add the product", ex);
        }
    }

    public void Update(int id, ProductDto productDto)
    {
        try
        {
            var product = new Product
            {
                Id = id,
                Name = productDto.Name,
                Price = productDto.Price
            };

            _repository.Update(product);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while update the product", ex);
        }
    }

    public bool Delete(int id)
    {
        try
        {
            return _repository.Delete(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while delete the product", ex);
        }
    }
}