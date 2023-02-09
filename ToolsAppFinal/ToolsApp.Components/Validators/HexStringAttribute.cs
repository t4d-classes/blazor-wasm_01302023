using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToolsApp.Components.Validators
{
  public class HexStringAttribute : ValidationAttribute
  {
    private Regex _hexStringRegex;
    private string _errorMessage;

    public HexStringAttribute(int hexStringLength = 6, string ErrorMessage = "")
    {
      _hexStringRegex = new Regex(
        "^[0-9a-fA-F]{" + hexStringLength.ToString() + "}$", RegexOptions.Compiled);
      _errorMessage = ErrorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      string inputValue = value.ToString();

      if (inputValue.Length == 0) {
        return ValidationResult.Success;
      }

      if (!_hexStringRegex.IsMatch(inputValue))
      {
        return new ValidationResult(
          _errorMessage.Length > 0
            ? _errorMessage
            : $"{validationContext.DisplayName} is not a valid hex value",
          new[] { validationContext.MemberName }
        );
      }

      return ValidationResult.Success;
    }
  }

}
