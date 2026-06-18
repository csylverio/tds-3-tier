using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

    [Fact]
    public void Validate_DoesNotThrow_WhenCurrentCultureUsesCommaDecimalSeparator()
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;

        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
            var dto = new CreateAccountDTO { Name = "Conta", Balance = 10 };

            var exception = Record.Exception(() => Validate(dto));

            Assert.Null(exception);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }

    private static List<ValidationResult> Validate(CreateAccountDTO dto)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(dto, new ValidationContext(dto), results, validateAllProperties: true);
        return results;
    }
}
