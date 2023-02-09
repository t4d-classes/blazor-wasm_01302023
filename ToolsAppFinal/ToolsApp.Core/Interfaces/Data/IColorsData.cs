using ToolsApp.Core.Interfaces.Models;

namespace ToolsApp.Core.Interfaces.Data;

public interface IColorsData
{
  Task<IEnumerable<IColor>> All();
  Task<IColor> Append(INewColor newColor);
  Task Remove(int colorId);
}
