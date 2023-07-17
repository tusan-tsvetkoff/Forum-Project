using Forum.Contracts.Authentication;
using Forum.Contracts.Comment;
using Forum.Contracts.Common;
using Forum.Contracts.Post;
using Forum.Contracts.Statistics;
using Forum.Contracts.Tags;
using Forum.Contracts.User;
using Forum.Server.Common.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Forum.Server.Common.Services;

public class APIClient : IAPIClient
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "https://localhost:7050/";
    public APIClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }
    // Statistics
    public Task<APIResponse<StatisticsResponse>> GetStatisticsAsync() => GetAsync<StatisticsResponse>("api/public/statistics");
    // Posts
    public Task<APIResponse<PostResponseListNew>> GetMostCommentedAsync() => GetAsync<PostResponseListNew>(
        "api/public/posts/most-commented");
    public Task<APIResponse<PostResponseListNew>> GetMostRecentAsync() => GetAsync<PostResponseListNew>("api/public/posts/most-recent");
    public Task<APIResponse<PostResponseListNew>> GetQueriedPostsAsync(GetPostsQueryParams postQueryParams, string token)
    {
        var queryString = QueryHelpers.AddQueryString("api/posts", new Dictionary<string, string?>());
        if (!string.IsNullOrWhiteSpace(postQueryParams.SearchTerm))
            queryString = QueryHelpers.AddQueryString(queryString, "searchTerm", postQueryParams.SearchTerm);
        if (!string.IsNullOrWhiteSpace(postQueryParams.SortColumn))
            queryString = QueryHelpers.AddQueryString(queryString, "sortColumn", postQueryParams.SortColumn);
        if (!string.IsNullOrWhiteSpace(postQueryParams.SortOrder))
            queryString = QueryHelpers.AddQueryString(queryString, "sortOrder", postQueryParams.SortOrder);
        if (!string.IsNullOrWhiteSpace(postQueryParams.Username))
            queryString = QueryHelpers.AddQueryString(queryString, "username", postQueryParams.Username);
        if (postQueryParams.Tags is not null && postQueryParams.Tags.Any())
            queryString = QueryHelpers.AddQueryString(queryString, "tags", postQueryParams.Tags);
        queryString = QueryHelpers.AddQueryString(queryString, "page", postQueryParams.Page.ToString());
        queryString = QueryHelpers.AddQueryString(queryString, "pageSize", postQueryParams.PageSize.ToString());

        return GetAsync<PostResponseListNew>(queryString, token);
    }
    public Task<APIResponse<PostResponse>> CreatePostRequestAsync(CreatePostRequest createPostRequest, string token) =>
        PostAsync<CreatePostRequest, PostResponse>("api/posts", createPostRequest, token);
    public Task<APIResponse<PostResponse>> GetPostByIdAsync(Guid postId, string token)
        => GetAsync<PostResponse>($"api/posts/{postId}", token);
    public Task<APIResponse<PostResponse>> UpdatePostRequestAsync(UpdatePostRequest updatePostRequest, Guid postId, string token) =>
        PatchAsync<UpdatePostRequest, PostResponse>($"api/posts/{postId}", updatePostRequest, token);
    public Task<APIResponse<PostResponse>> DeletePostRequestAsync(Guid postId, string token)
        => DeleteAsync<PostResponse>($"api/posts/{postId}", token);

    // Tags
    public Task<APIResponse<List<TagsResponse>>> GetTagsAsync() => GetAsync<List<TagsResponse>>("api/public/tags");
    public Task<APIResponse<TagsResponse>> CreateTagRequestAsync(CreateTagRequest createTagRequest) =>
        PostAsync<CreateTagRequest, TagsResponse>("api/tags", createTagRequest);

    // Authentication
    public Task<APIResponse<AuthenticationResponse>> RegisterRequestAsync(RegisterRequest register) =>
        PostAsync<RegisterRequest, AuthenticationResponse>("auth/register", register);

    public Task<APIResponse<AuthenticationResponse>> LoginRequestAsync(LoginRequest login) =>
        PostAsync<LoginRequest, AuthenticationResponse>("auth/login", login);

    // Comments
    public Task<APIResponse<CommentResponse>> CreateCommentRequestAsync(CreateCommentRequest createCommentRequest, Guid postId, string token) =>
        PostAsync<CreateCommentRequest, CommentResponse>($"api/posts/{postId}/comments", createCommentRequest, token);
    public Task<APIResponse<CommentResponse>> DeleteCommentRequestAsync(Guid commentId, Guid postId, string token) =>
        DeleteAsync<CommentResponse>($"api/posts/{postId}/comments/{commentId}", token);
    public Task<APIResponse<CommentResponse>> PatchCommentRequestAsync(Guid commentId, Guid postId, UpdateCommentRequest request, string token) =>
        PatchAsync<UpdateCommentRequest, CommentResponse>($"api/posts/{postId}/comments/{commentId}", request, token);
    public Task<APIResponse<ListCommentResponse>> GetCommentsAsync(GetCommentsQueryParams queryParams, string token, Guid postId)
    {
        var queryString = QueryHelpers.AddQueryString($"api/posts/{postId}/comments", new Dictionary<string, string?>());
        if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            queryString = QueryHelpers.AddQueryString(queryString, "searchTerm", queryParams.SearchTerm);
        if (!string.IsNullOrWhiteSpace(queryParams.SortColumn))
            queryString = QueryHelpers.AddQueryString(queryString, "sortColumn", queryParams.SortColumn);
        if (!string.IsNullOrWhiteSpace(queryParams.SortOrder))
            queryString = QueryHelpers.AddQueryString(queryString, "sortOrder", queryParams.SortOrder);
        if (!string.IsNullOrWhiteSpace(queryParams.Username))
            queryString = QueryHelpers.AddQueryString(queryString, "username", queryParams.Username);
        queryString = QueryHelpers.AddQueryString(queryString, "page", queryParams.Page.ToString());
        queryString = QueryHelpers.AddQueryString(queryString, "pageSize", queryParams.PageSize.ToString());

        return GetAsync<ListCommentResponse>(queryString, token);
    }

    public Task<APIResponse<ListAuthorCommentResponse>> GetAuthorCommentsAsync(GetCommentsQueryParams queryParams, string token, string authorId)
    {
        var queryString = QueryHelpers.AddQueryString($"api/users/{authorId}/comments", new Dictionary<string, string?>());
        if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            queryString = QueryHelpers.AddQueryString(queryString, "searchTerm", queryParams.SearchTerm);
        if (!string.IsNullOrWhiteSpace(queryParams.SortColumn))
            queryString = QueryHelpers.AddQueryString(queryString, "sortColumn", queryParams.SortColumn);
        if (!string.IsNullOrWhiteSpace(queryParams.SortOrder))
            queryString = QueryHelpers.AddQueryString(queryString, "sortOrder", queryParams.SortOrder);
        if (!string.IsNullOrWhiteSpace(queryParams.Username))
            queryString = QueryHelpers.AddQueryString(queryString, "username", queryParams.Username);
        queryString = QueryHelpers.AddQueryString(queryString, "page", queryParams.Page.ToString());
        queryString = QueryHelpers.AddQueryString(queryString, "pageSize", queryParams.PageSize.ToString());

        return GetAsync<ListAuthorCommentResponse>(queryString, token);
    }

    // Users
    public Task<APIResponse<AuthorResponse>> GetAuthorProfileByIdAsync(string authorId, string token) =>
        GetAsync<AuthorResponse>($"api/users/{authorId}", token);
    public Task<APIResponse<AuthorResponse>> UpdateUserAsync(Guid userId, string token, UpdateProfileRequest request) =>
        PatchAsync<UpdateProfileRequest, AuthorResponse>($"api/users/{userId}", request, token);
    public Task<APIResponse<AuthorResponse>> DeleteUserAsync(Guid userId, string token) =>
        DeleteAsync<AuthorResponse>($"api/users/{userId}", token);

    // Generic
    public Task<APIResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data, string token = "")
        where TResponse : class
        where TRequest : class
    {
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );

        return SendAsync<TResponse>(HttpMethod.Post, url, content, token);
    }

    private async Task<APIResponse<TResponse>> GetAsync<TResponse>(string url, string token = "") where TResponse : class
    {
        return await SendAsync<TResponse>(HttpMethod.Get, url, null, token);
    }

    private async Task<APIResponse<TResponse>> DeleteAsync<TResponse>(string url, string token = "") where TResponse : class
    {
        return await SendAsync<TResponse>(HttpMethod.Delete, url, null, token);
    }

    public Task<APIResponse<TResponse>> PatchAsync<TRequest, TResponse>(string url, TRequest data, string token = "")
        where TResponse : class
        where TRequest : class
    {
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");

        return SendAsync<TResponse>(HttpMethod.Patch, url, content, token);
    }

    private async Task<APIResponse<TResponse>> SendAsync<TResponse>(HttpMethod method, string url, HttpContent? content, string token = "") where TResponse : class
    {
        var request = new HttpRequestMessage(method, url)
        {
            Content = content
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        TResponse? responseData = null;
        var isSuccessful = false;
        string? errorMessage = null;

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            responseData = null;
            isSuccessful = true;
        }

        if (response.IsSuccessStatusCode && response.StatusCode is not HttpStatusCode.NoContent)
        {
            responseData = await response.Content.ReadFromJsonAsync<TResponse>();
            isSuccessful = true;
        }
        else
        {
            errorMessage = await response.Content.ReadAsStringAsync();
        }

        return new APIResponse<TResponse>(responseData, isSuccessful, errorMessage);
    }
}
