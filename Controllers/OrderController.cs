using dotnet_fancy_swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet_fancy_swagger.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
  public List<Order> Orders = new()
    {
        new(1, "Gadget", 99),
        new(2, "Gadget 2", 199),
        new(3, "Gadget 3", 299),
    };

    [HttpGet()]
    [SwaggerOperation(Tags = new[] { nameof(Tag.Orders) })]
    public IEnumerable<Order> Get() => Orders;

	[HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { nameof(Tag.Orders) })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<Order> GetById(int id)
	{
        var order = Orders.Find(u => u.id == id);
        
        if (order is null)
        {
            return NotFound();
        }

        return order;
	}

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id) => NoContent();

    [HttpPost]
    [SwaggerOperation(Tags = new[] { nameof(Tag.Orders) })]
    public ActionResult<Order> Create(Order orderRequest) => orderRequest;  
}
