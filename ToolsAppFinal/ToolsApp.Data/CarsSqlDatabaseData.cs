using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;
using ToolsApp.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ToolsApp.Data
{
  public class CarsSqlDatabaseData : ICarsData
  {
    private IMapper _mapper;
    private ToolsAppDbContext _toolsAppDbContext;

    public CarsSqlDatabaseData(ToolsAppDbContext toolsAppDbContext)
    {
      _toolsAppDbContext = toolsAppDbContext;

      var mapperConfig = new MapperConfiguration(config => {
        config.CreateMap<INewCar, CarDataModel>();
        config.CreateMap<ICar, CarDataModel>();
        config.CreateMap<CarDataModel, CarModel>().ReverseMap();
      });

      _mapper = mapperConfig.CreateMapper();
    }

    public async Task<IEnumerable<ICar>> All()
    {
      return await _toolsAppDbContext
        .Cars
        .Select(carDataModel => _mapper.Map<CarDataModel, CarModel>(carDataModel))
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<ICar> Append(INewCar newCar)
    {
      var newCarDataModel = _mapper.Map<CarDataModel>(newCar);

      await _toolsAppDbContext.AddAsync(newCarDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();

      return _mapper.Map<CarDataModel, CarModel>(newCarDataModel);
    }

    public async Task Replace(ICar car)
    {
      var carDataModel = _mapper.Map<CarDataModel>(car);
      _toolsAppDbContext.Update(carDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();
    }

    public async Task Remove(int carId)
    {
      var carDataModel = await _toolsAppDbContext.Cars.FindAsync(carId);
      _toolsAppDbContext.Cars.Remove(carDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();
    }
  }
}