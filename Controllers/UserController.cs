using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_BackEnd.Context;
using CRUD_BackEnd.Models;
using CRUD_BackEnd.Dtos;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

[ApiController]
[Route("api/[Controller]")]

public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    /*GET all*/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> getUsers()
    {
        var users = await _context.Users.ToListAsync();
        if(users == null) return NoContent();
        return Ok(users);
    }

    /*GET by id*/
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> getUser()
    {
        var user = await _context.Users.FindAsync();
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> postUser(UserCreateDto userDto)
    {
        
        var hasher = new PasswordHasher<User>();

        var newUser = new User
        {
            id = userDto.id,
            name = userDto.Name,
            email = userDto.email,
            passwordHash = userDto.password
        };

        _context.Users.Add(newUser);
        await  _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(getUser),
            new {id = newUser.id},
            userDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteUser(long id )
    {
        var userToDelete = await _context.Users.FindAsync(id);
        if(userToDelete == null)
        {
            return NotFound();
        }

        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
