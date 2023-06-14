namespace Forum.Api.Common.Helpers
{
    public interface IUserIdProvider
    {
        string GetUserId(string token);
    }
}
