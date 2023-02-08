using AutoMapper;
using Dapper;
using Microsoft.JSInterop;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;
using ToolsApp.Models;

namespace ToolsApp.Data
{
  public class ColorsDapperData: IColorsData
  {
    private IMapper _mapper;
    private ToolsAppDapperContext _dataContext;

    public ColorsDapperData(ToolsAppDapperContext dataContext)
    {
      _dataContext = dataContext;
      var mapperConfig = new MapperConfiguration(config => {
        config.CreateMap<INewColor, ColorDataModel>();
        config.CreateMap<IColor, ColorDataModel>();
        config.CreateMap<ColorDataModel, ColorModel>();
      });

      _mapper = mapperConfig.CreateMapper();
    }

    [JSInvokable]
    public async Task<IEnumerable<IColor>> All()
    {
      using var con = _dataContext.CreateConnection();

      var sql = "select Id, Name, HexCode from Color";
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
}
