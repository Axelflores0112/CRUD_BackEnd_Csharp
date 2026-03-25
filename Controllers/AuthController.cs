using Microsoft.AspNetCore.Mvc;
using CRUD_BackEnd.Context;
using CRUD_BackEnd.Models;
using CRUD_BackEnd.Dtos;
using CRUD_BackEnd.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[Controller]")]

public class Authcontroller : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public Authcontroller(AppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpGet("test")]
    public async Task<ActionResult> Test()
    {
        return Ok();
    }

    /*User Register*/
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserCreateDto userDTO)
    {

        var newUser = new User
        {
          id = userDTO.id,
          name = userDTO.name,
          email = userDTO.email,
          passwordHash = _passwordHasher.Hash(userDTO.password)
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(UserAuthDTO userDTO)
    {
        // Sacamos el usario que coincida con el email enviado desde el form
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.email == userDTO.email);

        if(user == null)
        {
            return Unauthorized("User not found");
        }

        bool validPassword = _passwordHasher.Verify(
            userDTO.password,
            user.passwordHash
        );

        if (!validPassword)
        {
            return Unauthorized("Password wrong");
        }

        return Ok("Login correcto");
    }
    
}
