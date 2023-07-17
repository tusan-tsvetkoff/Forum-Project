using Forum.Contracts.Authentication;
using Forum.Contracts.Comment;
using Forum.Contracts.Common;
using Forum.Contracts.Post;
using Forum.Contracts.Statistics;
using Forum.Contracts.Tags;
using Forum.Contracts.User;

namespace Forum.Server.Common.Interfaces;

public interface IAPIClient
{
    Task<APIResponse<CommentResponse>> CreateCommentRequestAsync(CreateCommentRequest createCommentRequest, Guid postId, string token);
    Task<APIResponse<PostResponse>> CreatePostRequestAsync(CreatePostRequest createPostRequest, string token);
    Task<APIResponse<TagsResponse>> CreateTagRequestAsync(CreateTagRequest createTagRequest);
    Task<APIResponse<CommentResponse>> DeleteCommentRequestAsync(Guid commentId, Guid postId, string token);
    Task<APIResponse<PostResponse>> DeletePostRequestAsync(Guid postId, string token);
    Task<APIResponse<PostResponseListNew>> GetMostCommentedAsync();
    Task<APIResponse<PostResponseListNew>> GetMostRecentAsync();
    Task<APIResponse<PostResponse>> GetPostByIdAsync(Guid postId, string token);
    Task<APIResponse<PostResponseListNew>> GetQueriedPostsAsync(GetPostsQueryParams postQueryParams, string token);
    Task<APIResponse<StatisticsResponse>> GetStatisticsAsync();
    Task<APIResponse<List<TagsResponse>>> GetTagsAsync();
    Task<APIResponse<AuthenticationResponse>> LoginRequestAsync(LoginRequest login);
    Task<APIResponse<CommentResponse>> PatchCommentRequestAsync(Guid commentId, Guid postId, UpdateCommentRequest request, string token);
    Task<APIResponse<AuthenticationResponse>> RegisterRequestAsync(RegisterRequest register);
    Task<APIResponse<PostResponse>> UpdatePostRequestAsync(UpdatePostRequest updatePostRequest, Guid postId, string token);
    Task <APIResponse<AuthorResponse>> GetAuthorProfileByIdAsync(string authorId, string token);
    Task<APIResponse<ListAuthorCommentResponse>> GetAuthorCommentsAsync(GetCommentsQueryParams queryParams, string token, string authorId);
    Task<APIResponse<ListCommentResponse>> GetCommentsAsync(GetCommentsQueryParams queryParams, string token, Guid postId);
    Task<APIResponse<AuthorResponse>> UpdateUserAsync(Guid userId, string token, UpdateProfileRequest request);
    Task<APIResponse<AuthorResponse>> DeleteUserAsync(Guid userId, string token);
}
