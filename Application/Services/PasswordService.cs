using System;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<string> _passwordHasher;

    public PasswordService(IPasswordHasher<string> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }


    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password can not be empty or null");
        }
        return _passwordHasher.HashPassword(string.Empty, password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword) )
        {
            throw new ArgumentException("Password can not be empty or null");
        }

        var result = _passwordHasher.VerifyHashedPassword(string.Empty, hashedPassword, password);
        return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
