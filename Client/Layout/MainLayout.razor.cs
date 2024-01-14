using Microsoft.AspNetCore.Components;
using Client.AppTheme;
using Client.Handlers;

namespace Client.Layout;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject] public LayoutService LayoutService { get; set; } = default!;
    protected override void OnInitialized()
    {        
        LayoutService?.SetBaseTheme(Theme.DocsTheme());
        LayoutService!.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured!;
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await LayoutService!.ApplyUserPreferences();
            LayoutService.OnMajorUpdateOccured();
            StateHasChanged();
        }
    }
   
    public void Dispose()
    {
        LayoutService!.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured!;
    }

    private void LayoutServiceOnMajorUpdateOccured(object sender, EventArgs e) => StateHasChanged();
}
