namespace GameStore.Business.Interface;

public interface IPlatformService : IBaseService<PlatformModel, PlatformModel>
{
    Task<List<PlatformModel>> GetByGameKeyAsync(string key);
}