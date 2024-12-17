using Microsoft.AspNetCore.Mvc;
using UserService.Helpers;
using UserService.Models;
using UserService.Repositories;


namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        var users = _repo.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var user = _repo.GetById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> Post([FromBody] User user)
    {
        // Hash the password before storing
        if (!string.IsNullOrWhiteSpace(user.Password))
        {
            user.Password = PasswordHasherHelper.HashPassword(user.Password);
        }
        
        var created = _repo.Add(user);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<User> Put(int id, [FromBody] User user)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();

        // Ensuring the ID remains consistent
        user.Id = id;

        // Hash the password only if it is provided
        if (!string.IsNullOrWhiteSpace(user.Password))
        {
            user.Password = PasswordHasherHelper.HashPassword(user.Password);
        }
        
        var updated = _repo.Update(user);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();

        _repo.Remove(id);
        return NoContent();
    }
}
