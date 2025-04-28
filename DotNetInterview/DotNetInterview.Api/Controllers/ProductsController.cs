using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<ProductDto>> Get()
    {
        try
        {
            var products = _service.GetAll();
            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> Get(int id)
    {
        try
        {
            var product = _service.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public ActionResult Add(CreateProductDto productDto)
    {
        try
        {
            _service.Add(productDto);
            return CreatedAtAction(nameof(Get), new { name = productDto.Name }, productDto);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, ProductDto productDto)
    {
        try
        {
            _service.Update(id, productDto);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}