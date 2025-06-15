using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.Products.Commands.CreateProduct;
using OrderSystem.Application.Products.Commands.DeleteProduct;
using OrderSystem.Application.Products.Commands.UpdateProduct;
using OrderSystem.Application.Products.Queries.GetAllProducts;
using OrderSystem.Application.Products.Queries.GetProductById;

namespace OrderSystem.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать новый продукт
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Обновить продукт
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand body)
    {
        if (id != body.Id)
            return BadRequest("ID в URL и теле запроса не совпадают.");

        await _mediator.Send(body);
        return NoContent();
    }

    /// <summary>
    /// Удалить продукт
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Получить продукт по Id
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    /// <summary>
    /// Получить список всех продуктов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }
}