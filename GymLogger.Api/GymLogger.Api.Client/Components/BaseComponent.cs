using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Components;

public abstract class BaseComponent : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] public required IDialogService DialogService { get; set; }
    [Inject] public required IToastService ToastService { get; set; }
}
