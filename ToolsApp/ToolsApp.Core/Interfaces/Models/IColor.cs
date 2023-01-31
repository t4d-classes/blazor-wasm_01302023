using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.Core.Interfaces.Models
{
  public interface IColor
  {
    int Id { get; set; }
    string Name { get; set; }
    string HexCode { get; set; }
  }
}
