using ToolsApp.Core.Interfaces.Models;

namespace ToolsApp.Models
{
  public class NewColor : INewColor
  {
    public string Name { get; set; }
    public string HexCode { get; set; }
  }

  public class Color : NewColor, IColor
  {
    public int Id { get; set; }
  }
}
