using System;
using Application.Dtos;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<UserResponse> createUser(UserRequest userRequest);
}
