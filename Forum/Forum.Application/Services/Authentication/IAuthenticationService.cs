using System;


namespace Forum.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string email, string password);
        AuthenticationResult Register(string firstName,string lastName, string username, string email, string password);
    }
}
