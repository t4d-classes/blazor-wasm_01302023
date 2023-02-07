using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ToolsApp.Data
{
  public class ToolsAppDapperContext
  {

    private readonly string _connectionString;

    public ToolsAppDapperContext(IConfiguration configuration) {
      _connectionString = configuration.GetConnectionString("ToolsAppDb");
    }

    public ToolsAppDapperContext()
    {
      _connectionString = "";
    }

    public virtual IDbConnection CreateConnection() => new SqlConnection(_connectionString);
  }
}
