using AutoMapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;
using ToolsApp.Data.Models;
using ToolsApp.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;

namespace ToolsApp.Data
{
  public class CarsInMemoryData : ICarsData
  {
    private List<CarDataModel> _cars = new() {
      new() { Id = 1, Make="Ford", Model="Fusion Hybrid", Year=2020, Color="Blue", Price=45000 },
      new() { Id = 2, Make="Tesla", Model="S", Year=2022, Color="Red", Price=115000 },
    };

    private IMapper _mapper;

    public CarsInMemoryData() {
      var mapperConfig = new MapperConfiguration(config => {
        config.CreateMap<INewCar, CarDataModel>();
        config.CreateMap<ICar, CarDataModel>();
        config.CreateMap<CarDataModel, CarModel>().ReverseMap();
      });

      _mapper = mapperConfig.CreateMapper();
    }

    public Task<IEnumerable<ICar>> All()
    {
      return Task.FromResult(_cars.Select(
          carDataModel => _mapper.Map<CarDataModel, CarModel>(carDataModel)
        ).AsEnumerable<ICar>());
    }

    public Task<ICar> Append(INewCar newCar)
    {
      var newCarDataModel = _mapper.Map<CarDataModel>(newCar);
      newCarDataModel.Id = _cars.Any() ? _cars.Max(c => c.Id) + 1 : 1;

      _cars.Add(newCarDataModel);

      return Task.FromResult(
        _mapper.Map<CarDataModel, CarModel>(newCarDataModel) as ICar);
    }

    public Task Remove(int carId)
    {
      var carIndex = _cars.FindIndex(car => car.Id == carId);
      if (carIndex == -1)
      {
        throw new IndexOutOfRangeException("Car not found");
      }

      _cars.RemoveAt(carIndex);
      return Task.CompletedTask;
    }

    public Task Replace(ICar car)
    {
      var carIndex = _cars.FindIndex(c => c.Id == car.Id);
      if (carIndex == -1)
      {
        throw new IndexOutOfRangeException("Car not found");
      }
      _cars[carIndex] = _mapper.Map<CarDataModel>(car);
      return Task.CompletedTask;
    }
  }
}
