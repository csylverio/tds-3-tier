using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyFinance.Api.Controllers;
using MyFinance.Business.Service;

namespace MyFinance.Tests.Api.Controllers;

public class AccountControllerTests
{
    [Fact]
    public async Task GetById_ReturnsNotFoundProblemDetails_WhenAccountDoesNotExist()
    {
        var serviceMock = new Mock<IAccountService>();
        serviceMock.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Conta inválida!"));
        var controller = new AccountController(serviceMock.Object);

        var result = await controller.GetById(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(notFound.Value);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
    }

    [Fact]
    public void Delete_HasPrivilegedRoleAuthorization()
    {
        var method = typeof(AccountController).GetMethod(nameof(AccountController.Delete));

        var authorizeAttribute = Assert.Single(method!.GetCustomAttributes(typeof(AuthorizeAttribute), inherit: false)
            .Cast<AuthorizeAttribute>());
        Assert.Equal("Privileged", authorizeAttribute.Roles);
    }
}
