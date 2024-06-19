using GymLogger.Api.Client.HttpServices;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Components;

public abstract class BaseCreatePage<TDto, THttpService> : BaseComponent
    where THttpService : ICreateHttpService<TDto>
    where TDto : new()
{
    [Inject] protected THttpService HttpService { get; set; }
    protected TDto Model { get; set; } = new TDto();
    protected abstract string SuccessCreateMessage { get; }
    protected abstract string RedirectPath { get; }

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    protected virtual async Task HandleValidSubmit()
    {
        //this.ShowSpinner = true;
        var response = await this.HttpService.CreateAsync(this.Model);
        if (response.IsSuccessStatusCode)
        {
            //this.Snackbar.Add(this.SuccessCreateMessage, Severity.Success);
            this.NavigationManager.NavigateTo(this.RedirectPath);
        }
        else
        {
            //this.ShowSpinner = false;
            //this.ErrorMessage = "Dogodila se greška prilikom stvaranja zapisa!";
            //this.ShowErrorMessage = true;
        }
    }
}
