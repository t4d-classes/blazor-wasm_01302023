using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace ToolsApp.Data
{
  public class ColorsSqlDatabaseData : IColorsData
  {
    private IMapper _mapper;
    private ToolsAppDbContext _toolsAppDbContext;

    public ColorsSqlDatabaseData(ToolsAppDbContext toolsAppDbContext)
    {
      _toolsAppDbContext = toolsAppDbContext;

      var mapperConfig = new MapperConfiguration(config => {
        config.CreateMap<INewColor, ColorDataModel>();
        config.CreateMap<IColor, ColorDataModel>();
        config.CreateMap<ColorDataModel, ColorModel>().ReverseMap();
      });

      _mapper = mapperConfig.CreateMapper();
    }

    [JSInvokable]
    public async Task<IEnumerable<IColor>> All()
    {
      return await _toolsAppDbContext
        .Colors
        .Select(colorDataModel => _mapper.Map<ColorDataModel, ColorModel>(colorDataModel))
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<IColor> Append(INewColor newColor)
    {
      var newColorDataModel = _mapper.Map<ColorDataModel>(newColor);

      await _toolsAppDbContext.AddAsync(newColorDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();

      return _mapper.Map<ColorDataModel, ColorModel>(newColorDataModel);
    }

    public async Task Replace(IColor color)
    {
      var colorDataModel = _mapper.Map<ColorDataModel>(color);
      _toolsAppDbContext.Update(colorDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();
    }

    public async Task Remove(int colorId)
    {
      var colorDataModel = await _toolsAppDbContext.Colors.FindAsync(colorId);
      _toolsAppDbContext.Colors.Remove(colorDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();
    }
  }
}
