using System;
using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<UserResponse> CreateUser(UserRequest userRequest);
    public Task<User?> GetUserByUsername(string username);
    public Task<bool> Login(UserRequest userRequest);
}