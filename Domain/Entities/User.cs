using System;

namespace Domain.Entities;

public class User
{
    public int user_id { get; set; }
    public string username { get; set; } = string.Empty;
    public string hash_password { get; set; } = string.Empty;
}
