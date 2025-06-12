namespace MyFinance.Api.Authentication;

public enum AccessType
{
    /// <summary>
    /// Acesso público
    /// </summary>
    Public,
    /// <summary>
    /// Acesso restrito
    /// </summary>
    Restricted,
    /// <summary>
    /// Acesso privilegiado
    /// </summary>
    Privileged,
    /// <summary>
    /// Acesso de visitante
    /// </summary>
    Guest,
    /// <summary>
    /// Acesso de moderador
    /// </summary>
    Moderator
}
