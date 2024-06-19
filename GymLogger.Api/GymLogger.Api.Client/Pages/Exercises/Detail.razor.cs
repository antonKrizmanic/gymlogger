using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises;

public partial class Detail : ComponentBase
{
    [Parameter] public Guid Id { get; set; }
}
