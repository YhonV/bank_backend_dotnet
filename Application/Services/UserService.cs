using System.Threading.Channels;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infra.ConnectionDB;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{

    private readonly PostgresContext _context;
    private readonly IPasswordService _passwordService;

    public UserService(PostgresContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    // CreateUser method
    public async Task<UserResponse> CreateUser(UserRequest userRequest)
    {
        if (string.IsNullOrWhiteSpace(userRequest.username))
        {
            throw new ArgumentException("The username is required!");
        }

        string hashedPassword = _passwordService.HashPassword(userRequest.password);

        var user = new User
        {
            username = userRequest.username,
            hash_password = hashedPassword
        };

        _context.user.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Id = user.user_id
        };
    }

    // Login method
    public async Task<bool> Login(UserRequest userRequest)
    {
        if (string.IsNullOrWhiteSpace(userRequest.username) || string.IsNullOrWhiteSpace(userRequest.password))
        {
            return false;
        }
        User? user = await GetUserByUsername(userRequest.username) ?? throw new Exception("User not found");

        bool isValidPassword = _passwordService.VerifyPassword(userRequest.password, user.hash_password);

        if(!isValidPassword){ return false; }

        return isValidPassword;
    }

    // GetUserByUsername method
    public async Task<User?> GetUserByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("The username is required!");
        }
        return await _context.user.FirstOrDefaultAsync(u => u.username == username);
    }
}
