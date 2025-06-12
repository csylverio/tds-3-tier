using System;

namespace MyFinance.Api.Authentication;

public class Login
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public AccessType AccessType { get; set; }
}
