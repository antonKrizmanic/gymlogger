using GymLogger.Shared.Services.Generics;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Components;

public abstract class BaseCreatePage<TCreateDto, TDto, THttpService> : BaseComponent
    where THttpService : ICreateHttpService<TCreateDto, TDto>
    where TCreateDto : new()
{
    [Inject] protected THttpService HttpService { get; set; }
    protected TCreateDto Model { get; set; } = new TCreateDto();
    protected abstract string SuccessCreateMessage { get; }
    protected abstract string RedirectPath { get; }

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();

    protected virtual async Task HandleValidSubmit()
    {
        //this.ShowSpinner = true;
        try
        {
            var response = await this.HttpService.CreateAsync(this.Model);
            this.NavigationManager.NavigateTo(this.RedirectPath);
        }
        catch (Exception)
        {
            //this.ShowSpinner = false;
            //this.ErrorMessage = "Dogodila se greška prilikom stvaranja zapisa!";
            //this.ShowErrorMessage = true;
        }
    }
}
