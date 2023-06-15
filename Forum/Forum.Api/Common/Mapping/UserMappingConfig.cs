using Forum.Application.Users.Commands.UpdateProfile;
using Forum.Contracts.User;
using Mapster;

namespace Forum.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateProfileRequest Request, Guid UserId), UpdateProfileCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}
