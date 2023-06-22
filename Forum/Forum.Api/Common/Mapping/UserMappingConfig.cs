using Forum.Application.Users.Commands.Delete;
using Forum.Application.Users.Commands.UpdateProfile;
using Forum.Application.Users.Queries;
using Forum.Contracts.Common;
using Forum.Contracts.User;
using Forum.Data.AuthorAggregate;
using Forum.Data.UserAggregate;
using Mapster;

namespace Forum.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateProfileRequest Request, Guid UserId), UpdateProfileCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Guid, DeleteUserCommand>()
            .Map(dest => dest.UserId, src => src);

        config.NewConfig<GetUserQueryParams, GetUsersQuery>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.SearchTerm, src => src.SearchTerm)
            .Map(dest => dest.SortColumn, src => src.SortColumn)
            .Map(dest => dest.SortOrder, src => src.SortOrder);

        config.NewConfig<(User, Author), UserResponse>()
            .Map(dest => dest.Id, src => src.Item1.Id.Value.ToString())
            .Map(dest => dest.PostCount, src => src.Item2.PostIds.Count)
            .Map(dest => dest.CommentCount, src => src.Item2.CommentIds.Count);

        // Keeping it for now. Might be useful later
        config.NewConfig<(int Page, int PageSize, int TotalCount, bool HasNextPage, bool HasPreviousPage), PageInfo>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.TotalCount, src => src.TotalCount)
            .Map(dest => dest.HasNextPage, src => src.HasNextPage)
            .Map(dest => dest.HasPreviousPage, src => src.HasPreviousPage);
    }
}
