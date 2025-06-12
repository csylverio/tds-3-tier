using System;

namespace MyFinance.Api.DTOs;


/// <summary>
/// Representa objeto de requisição de login
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// Nome do usuário
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Senha do usuário
    /// </summary>
    public required string Password { get; set; }
}
