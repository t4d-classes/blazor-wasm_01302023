using ToolsApp.Core.Interfaces.Models;

namespace ToolsApp.Core.Interfaces.Data;

public interface ICarsData
{
  Task<IEnumerable<ICar>> All();
  Task<ICar> Append(INewCar newCar);
  Task Replace(ICar car);
  Task Remove(int carId);
}
