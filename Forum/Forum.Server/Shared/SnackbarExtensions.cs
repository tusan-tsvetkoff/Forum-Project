using Forum.Server.Common;
using MudBlazor;

namespace Forum.Server.Shared;

public static class SnackbarExtensions
{
    public static bool HandleResponse<TResponse>(
        this ISnackbar notifier,
        APIResponse<TResponse> response,
        string? successMessage = null)
         where TResponse : class
    {
        if (response.Success)
        {
            if (successMessage is not null)
            {
                notifier.Add(successMessage, Severity.Success);
            }

            return true;
        }
        else
        {
            notifier.Add(response.ErrorMessage!, Severity.Error);
            return false;
        }
    }
}
