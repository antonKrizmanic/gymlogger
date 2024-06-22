using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Components;

public abstract class BaseComponent : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; }
}
