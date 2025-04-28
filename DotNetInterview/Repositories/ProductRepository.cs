public class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products = new List<Product>();

    private int GenerateNextId()
    {
        return _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
    }

    public List<Product> GetAll()
    {
        try
        {
            return _products;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while retrieving all products", ex);
        }
    }

    public Product GetById(int id)
    {
        try
        {
            return _products.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new Exception("Error fetching product by ID", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while retrieving the product", ex);
        }
    }

    public void Add(Product product)
    {
        try
        {
            product.Id = GenerateNextId();
            _products.Add(product);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while add the product", ex);
        }
    }

    public void Update(Product product)
    {
        try
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
            }
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
            int removedCount = _products.RemoveAll(p => p.Id == id);
            return removedCount > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while delete the product", ex);
        }
    }

}