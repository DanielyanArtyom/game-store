namespace GameStore.Data.Interface;

public interface IUserRespository: IRepository<User>
{
    Task<User> GetUserFullDataAsync(string login, CancellationToken ct = default);
}