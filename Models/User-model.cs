namespace CRUD_BackEnd.Models;

public class User
{
    public int id {get; set;}
    public required string name {get; set;}
    public required string email {get; set;}
    public required string passwordHash {get; set;}
}