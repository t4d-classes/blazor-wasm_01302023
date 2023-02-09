namespace ToolsApp.Core.Interfaces.Models
{
  public interface INewColor
  {
    string Name { get; set; }
    string HexCode { get; set; }
  }

  public interface IColor: INewColor
  {
    int Id { get; set; }
  }
}
