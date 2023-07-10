namespace Forum.Server.Common.Interfaces;

public interface ITokenStorageService
{
    Task StoreToken(string token);
    Task RemoveToken();
    Task<string> RetrieveToken();
}
