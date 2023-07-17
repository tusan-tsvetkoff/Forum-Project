using MudBlazor;

namespace Forum.Server.Common;

public class APIResponse<TResponse> where TResponse : class
{
    public APIResponse(TResponse? model, bool success, string? errorMessage)
    {
        Model = model;
        Success = success;
        ErrorMessage = errorMessage;
    }

    public TResponse? Model { get; }
    public bool Success { get; }
    public string? ErrorMessage { get; }
}


