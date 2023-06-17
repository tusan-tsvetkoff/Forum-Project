using FluentValidation;

namespace Forum.Application.Posts.Queries.ListPosts;

public class ListPostsQueryValidator : AbstractValidator<ListPostsQuery>
{
    public ListPostsQueryValidator()
    {
        // TODO: Review this
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(10);
    }

}
