using AutoMapper;
using Dapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;

namespace ToolsApp.Data;

public class CarsDapperData: ICarsData
{

  private IMapper _mapper;
  private ToolsAppDapperContext _dataContext;
  public CarsDapperData(ToolsAppDapperContext dataContext)
  {
    _dataContext = dataContext;
    var mapperConfig = new MapperConfiguration(config => {
      config.CreateMap<INewCar, CarDataModel>();
      config.CreateMap<ICar, CarDataModel>();
      config.CreateMap<CarDataModel, CarModel>();
    });

    _mapper = mapperConfig.CreateMapper();
  }

  public async Task<IEnumerable<ICar>> All()
  {
    using var con = _dataContext.CreateConnection();

    var sql = "select Id, Make, Model, Year, Color, Price from Car";
    var cars = await con.QueryAsync<CarDataModel>(sql);

    return cars
      .Select(car => _mapper.Map<CarDataModel, CarModel>(car))
      .AsEnumerable<ICar>();
  }

  public async Task<ICar> Append(INewCar newCar)
  {
    using var con = _dataContext.CreateConnection();

    var carData = await con.QueryAsync<CarDataModel>(
      "[InsertCar]",
      newCar,
      commandType: System.Data.CommandType.StoredProcedure
    );

    return _mapper.Map<CarDataModel, CarModel>(carData.Single()) as ICar;
  }

  public async Task Remove(int carId)
  {
    using var con = _dataContext.CreateConnection();

    await con.ExecuteAsync(
      "[DeleteCar]",
      new { Id = carId },
      commandType: System.Data.CommandType.StoredProcedure
    );
  }

  public async Task Replace(ICar car)
  {
    using var con = _dataContext.CreateConnection();

    await con.ExecuteAsync(
      "[UpdateCar]",
      car,
      commandType: System.Data.CommandType.StoredProcedure
    );
  }
}