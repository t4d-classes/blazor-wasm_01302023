using ToolsApp.Core.Interfaces.Models;

namespace ToolsApp.Models
{
  public class Color : IColor
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string HexCode { get; set; }
  }
}