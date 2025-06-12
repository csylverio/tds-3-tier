using System;
using MyFinance.Api.Authentication;

namespace MyFinance.Api.DTOs;

public class Login
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public AccessType AccessType { get; set; }
}
