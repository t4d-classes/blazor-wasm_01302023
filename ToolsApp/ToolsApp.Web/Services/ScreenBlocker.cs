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
      // window.$.blockUI() // which displays a div that is styled to block ui interaction
      await _js.InvokeVoidAsync("$.blockUI");
    }

    public async Task UnblockScreen()
    {
      // window.$.unblockUI() // which displays a div that is styled to unblock ui interaction
      await _js.InvokeVoidAsync("$.unblockUI");
    }

  }
}
