using Xunit;
using Bunit;

using ToolsApp.Components.CarTool;
using ToolsApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.Tests.Components.CarTool
{
  public class CarViewRowCSharpTest: TestContext
  {
    [Fact]
    public void CarViewRowComponentRendersCorrectly()
    {
      // Arrange

      var car = new Car {
        Id = 1,
        Make = "Ford",
        Model = "Fusion Hybrid",
        Year = 2020,
        Color = "blue",
        Price = 45000.0M
      };

      // Act

      var cut = RenderComponent<CarViewRow>(parameters => {
        parameters.Add(p => p.Car, car);
      });

      // Assert

      cut.MarkupMatches(@"<tr>
        <td>1</td>
        <td>Ford</td>
        <td>Fusion Hybrid</td>
        <td>2020</td>
        <td>blue</td>
        <td>45000.0</td>
        <td>
          <button type=""button"">Edit</button>
          <button type=""button"">Delete</button>
        </td>
      </tr>");
    }
  }
}
