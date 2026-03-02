namespace CRUD_BackEnd.Dtos;

public class UserCreateDto
{
    public int id {get; set;}
    public required string Name {get; set;}
    public required string email {get; set;}
    public required string password {get; set;}

}