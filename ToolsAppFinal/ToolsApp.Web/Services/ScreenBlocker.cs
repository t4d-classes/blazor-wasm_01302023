using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ToolsApp.Web.Services
{
  public class ScreenBlocker
  {
    private readonly IJSRuntime _js;

    public ScreenBlocker(IJSRuntime js)
    {
      _js = js;
    }

    public async Task BlockScreen()
    {
      await _js.InvokeVoidAsync("$.blockUI");
    }

    public async Task UnblockScreen()
    {
      await _js.InvokeVoidAsync("$.unblockUI");
    }
  }
}
