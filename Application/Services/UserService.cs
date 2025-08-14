using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infra.ConnectionDB;

namespace Application.Services;

public class UserService : IUserService
{

    private readonly PostgresContext _context;

    public UserService(PostgresContext context)
    {
        _context = context;
    }


    public async Task<UserResponse> createUser(UserRequest userRequest)
    {
        if (string.IsNullOrWhiteSpace(userRequest.username))
        {
            throw new ArgumentException("The username is required!");
        }

        var user = new User
        {
            Username = userRequest.username
        };

        _context.user.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Id = user.Id
        };
    }
}
