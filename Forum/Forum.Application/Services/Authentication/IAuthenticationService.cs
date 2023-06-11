using ErrorOr;
using System;


namespace Forum.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
        ErrorOr<AuthenticationResult> Register(string firstName,string lastName, string username, string email, string password);
    }
}
