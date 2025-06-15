using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.Customers.Commands.CreateCustomer;
using OrderSystem.Application.Customers.Commands.DeleteCustomer;
using OrderSystem.Application.Customers.Commands.UpdateCustomer;
using OrderSystem.Application.Customers.Queries.GetAllCustomer;
using OrderSystem.Application.Customers.Queries.GetCustomerByEmail;
using OrderSystem.Application.Customers.Queries.GetCustomerById;

namespace OrderSystem.API.Controllers;


[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать новый продукт
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Обновить продукт
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand body)
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
        await _mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Получить продукт по Id
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(product);
    }
    
    /// <summary>
    /// Получить продукт по Email
    /// </summary>
    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var customer = await _mediator.Send(new GetCustomerByEmailQuery(email));
        return Ok(customer);
    }

    /// <summary>
    /// Получить список всех продуктов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllCustomerQuery());
        return Ok(products);
    }
}