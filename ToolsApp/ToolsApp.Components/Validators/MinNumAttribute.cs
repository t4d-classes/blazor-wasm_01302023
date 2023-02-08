using System;
using System.ComponentModel.DataAnnotations;

namespace ToolsApp.Components.Validators
{
  public class MinNumAttribute: ValidationAttribute
  {
    private Decimal _minNum;

    public MinNumAttribute(double minNum = 0)
    {
      _minNum= Convert.ToDecimal(minNum);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      if (value is null)
      {
        return ValidationResult.Success;
      }

      var valueString = value.ToString();

      if (valueString is null || valueString.Length == 0)
      {
        return ValidationResult.Success;
      }

      if (!Decimal.TryParse(value.ToString(), out decimal valueDouble))
      {
        return new ValidationResult($"{validationContext.DisplayName} is not a number.");
      }

      if (valueDouble < _minNum) {
        return new ValidationResult($"{validationContext.DisplayName} cannot be less than {_minNum}.");
      }

      return ValidationResult.Success;
    }





  }
}
