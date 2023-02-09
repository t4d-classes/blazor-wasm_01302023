using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;
using ToolsApp.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;
using AutoMapper;
using Microsoft.JSInterop;

namespace ToolsApp.Data
{
  public class ColorsInMemoryData : IColorsData
  {
    private IMapper _mapper;

    private List<ColorDataModel> _colors { get; set; } = new List<ColorDataModel>() {
      new ColorDataModel() { Id = 1, Name = "red", HexCode = "ff0000" },
      new ColorDataModel() { Id = 2, Name = "green", HexCode = "00ff00" },
      new ColorDataModel() { Id = 3, Name = "blue", HexCode = "0000ff" },
    };

    public ColorsInMemoryData()
    {
      var mapperConfig = new MapperConfiguration(config => {
        config.CreateMap<INewColor, ColorDataModel>();
        config.CreateMap<IColor, ColorDataModel>();
        config.CreateMap<ColorDataModel, ColorModel>().ReverseMap();
      });

      _mapper = mapperConfig.CreateMapper();
    }

    [JSInvokable]
    public Task<IEnumerable<IColor>> All()
    {
      return Task.FromResult(
        _colors.Select(
          colorDataModel => _mapper.Map<ColorDataModel, ColorModel>(colorDataModel)
        )
        .AsEnumerable<IColor>()
      );
    }

    public Task<IColor> Append(INewColor newColor)
    {
      var newColorDataModel = _mapper.Map<ColorDataModel>(newColor);
      newColorDataModel.Id = _colors.Any() ? _colors.Max(c => c.Id) + 1 : 1;

      _colors.Add(newColorDataModel);

      return Task.FromResult(
        _mapper.Map<ColorDataModel, ColorModel>(newColorDataModel) as IColor);
    }

    public Task Remove(int colorId)
    {
      var colorIndex = _colors.FindIndex(color => color.Id == colorId);
      if (colorIndex == -1)
      {
        throw new IndexOutOfRangeException("Color not found");
      }

      _colors.RemoveAt(colorIndex);
      return Task.CompletedTask;
    }
  }
}