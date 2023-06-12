using Forum.Application.Authentication.Commands.Register;
using Forum.Application.Authentication.Common;
using Forum.Application.Authentication.Queries.Login;
using Forum.Contracts.Authentication;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Forum.Api.Common.Mapping;




public class AuthenticationMappingConfig : IRegister
{
    /// <summary>
    /// Registers type mappings for authenticating the user during registration or login using the provided <see cref="TypeAdapterConfig"/>.
    /// </summary>
    /// <param name="config">The <see cref="TypeAdapterConfig"/> object used for configuring type mappings.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value.ToString())
            .Map(dest => dest, src => src.User);
    }
}
