using System;
using System.ComponentModel.DataAnnotations;

namespace ToolsApp.Components.Validators
{
  public class MinNumAttribute : ValidationAttribute
  {
    private decimal _minNum;

    public MinNumAttribute(double minNum = 0)
    {
      _minNum = Convert.ToDecimal(minNum);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (Convert.ToDecimal(value) < _minNum)
      {
        return new ValidationResult($"{validationContext.DisplayName} cannot be less than {_minNum}.");
      }

      return ValidationResult.Success;
    }
  }

}
