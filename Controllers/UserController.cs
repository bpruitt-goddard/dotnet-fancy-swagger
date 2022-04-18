using dotnet_fancy_swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace dotnet_fancy_swagger.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public List<User> Users = new()
    {
        new(1, "Brian"),
        new(2, "Byron"),
        new(3, "Bryan"),
    };

    [HttpGet()]
    [SwaggerOperation(Tags = new[] { nameof(Tag.RetrievingUsers) })]
    public IEnumerable<User> Get() => Users;

	[HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { nameof(Tag.RetrievingUsers), nameof(Tag.Orders) })]
	public ActionResult<User> GetById(int id)
	{
        var user = Users.Find(u => u.id == id);
        
        return (user is null) ? NotFound() : user;
	}

    [HttpDelete]
    public ActionResult Delete(int id) => NoContent();

    [HttpPost]
    [SwaggerOperation(Tags = new[] { nameof(Tag.RetrievingUsers) })]
    public ActionResult<User> Create(User userRequest) => userRequest;
}