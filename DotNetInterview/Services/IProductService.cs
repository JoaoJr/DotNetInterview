public interface IProductService
{
    List<ProductDto> GetAll();
    ProductDto GetById(int id);
    void Add(CreateProductDto productDto);
    void Update(int id, ProductDto productDto);
    bool Delete(int id);
}