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
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        if(users == null) return NoContent();
        return Ok(users);
    }

    /*GET by id*/
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser()
    {
        var user = await _context.Users.FindAsync();
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(long id )
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
