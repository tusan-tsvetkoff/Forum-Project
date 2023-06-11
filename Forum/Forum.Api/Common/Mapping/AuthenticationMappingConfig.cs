using Forum.Application.Authentication.Commands.Register;
using Forum.Application.Authentication.Common;
using Forum.Application.Authentication.Queries.Login;
using Forum.Contracts.Authentication;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Forum.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}
