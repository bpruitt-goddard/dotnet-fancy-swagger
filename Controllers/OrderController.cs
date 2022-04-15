using dotnet_fancy_swagger.Models;
using Microsoft.AspNetCore.Mvc;

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
    public IEnumerable<Order> Get() => Orders;

	[HttpGet("{id}")]
	public ActionResult<Order> GetById(int id)
	{
        var order = Orders.Find(u => u.id == id);
        
        return (order is null) ? NotFound() : order;
	}

    [HttpDelete]
    public ActionResult Delete(int id) => NoContent();

    [HttpPost]
    public ActionResult<Order> Create(Order orderRequest) => orderRequest;  
}