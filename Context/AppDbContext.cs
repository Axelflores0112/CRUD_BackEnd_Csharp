using CRUD_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
namespace CRUD_BackEnd.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users {get; set;}
}