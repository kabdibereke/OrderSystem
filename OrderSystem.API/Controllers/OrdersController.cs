using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.Orders.Commands.CancelOrder;
using OrderSystem.Application.Orders.Commands.ConfirmOrder;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Application.Orders.Commands.ShipOrder;
using OrderSystem.Application.Orders.Commands.UpdateOrder;
using OrderSystem.Application.Orders.Queries.GetAllByCustomerIdOrder;
using OrderSystem.Application.Orders.Queries.GetAllOrder;
using OrderSystem.Application.Orders.Queries.GetByIdOrder;

namespace OrderSystem.API.Controllers;


[ApiController]
[Route("api/orders")]
public class OrdersController: ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Обновить заказ
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand body)
    {
        if (id != body.OrderId)
            return BadRequest("ID в URL и теле запроса не совпадают.");

        await _mediator.Send(body);
        return NoContent();
    }
    
    /// <summary>
    /// Получить продукт по Id
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetByIdOrderQuery(id));
        return Ok(product);
    }
    
    
    /// <summary>
    /// Получить список всех заказов по покупателю
    /// </summary>
    [HttpGet("by-customer/{customerId:guid}")]
    public async Task<IActionResult> GetAllByCustomerId(Guid id)
    {
        var products = await _mediator.Send(new GetAllByCustomerIdOrderQuery(id));
        return Ok(products);
    }
    

    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllOrderQuery());
        return Ok(products);
    }
    
    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        await _mediator.Send(new ConfirmOrderCommand(id));
        return NoContent();
    }
    
    [HttpPost("{id:guid}/ship")]
    public async Task<IActionResult> Ship(Guid id)
    {
        await _mediator.Send(new ShipOrderCommand(id));
        return NoContent();
    }
    
    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand(id));
        return NoContent();
    }
}