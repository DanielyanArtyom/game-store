namespace GameStore.Business.Interface;

public interface IPublisherService : IBaseService<PublisherModel, PublisherModel>
{
    Task<PublisherModel> GetByCompanyNameAsync(string companyName);
    Task<PublisherModel> GetByGameKeyAsync(string key);
}