using AutoMapper;
using Dapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;

using Microsoft.JSInterop;

namespace ToolsApp.Data;

public class ColorsDapperData : IColorsData
{
  private ToolsAppDapperContext _dataContext;
  private IMapper _mapper;

  public ColorsDapperData(ToolsAppDapperContext dataContext)
  {
    _dataContext = dataContext;
    var mapperConfig = new MapperConfiguration(config => {
      config.CreateMap<ColorDataModel, ColorModel>().ReverseMap();
    });
    _mapper = mapperConfig.CreateMapper();
  }

  [JSInvokable]
  public async Task<IEnumerable<IColor>> All()
  {
    using var con = _dataContext.CreateConnection();

    var sql = "select Id, Name, Hexcode from Color";
    var colors = await con.QueryAsync<ColorDataModel>(sql);

    return colors
      .Select(color => _mapper.Map<ColorDataModel, ColorModel>(color))
      .AsEnumerable<IColor>();
  }

  public async Task<IColor> Append(INewColor newColor)
  {
    using var con = _dataContext.CreateConnection();

    var colorData = await con.QueryAsync<ColorDataModel>(
      "[InsertColor]",
      newColor,
      commandType: System.Data.CommandType.StoredProcedure
    );

    return _mapper.Map<ColorDataModel, ColorModel>(colorData.Single()) as IColor;
  }


  public async Task Remove(int colorId)
  {
    using var con = _dataContext.CreateConnection();

    await con.ExecuteAsync(
      "[DeleteColor]",
      new { Id = colorId },
      commandType: System.Data.CommandType.StoredProcedure
    );
  }

}
