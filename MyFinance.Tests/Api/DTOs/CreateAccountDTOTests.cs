using System.ComponentModel.DataAnnotations;
using MyFinance.Api.DTOs;

namespace MyFinance.Tests.Api.DTOs;

public class CreateAccountDTOTests
{
    [Fact]
    public void Validate_ReturnsError_WhenNameIsMissing()
    {
        var dto = new CreateAccountDTO { Name = null!, Balance = 100 };

        var validationResults = Validate(dto);

        Assert.Contains(validationResults, result => result.MemberNames.Contains(nameof(CreateAccountDTO.Name)));
    }

    [Fact]
    public void Validate_ReturnsError_WhenBalanceIsNegative()
    {
        var dto = new CreateAccountDTO { Name = "Conta", Balance = -1 };

        var validationResults = Validate(dto);

        Assert.Contains(validationResults, result => result.MemberNames.Contains(nameof(CreateAccountDTO.Balance)));
    }

    private static List<ValidationResult> Validate(CreateAccountDTO dto)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(dto, new ValidationContext(dto), results, validateAllProperties: true);
        return results;
    }
}
