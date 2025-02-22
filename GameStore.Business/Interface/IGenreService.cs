namespace GameStore.Business.Interface;

public interface IGenreService : IBaseService<GenreModel, GenreModel>
{
    Task<List<GenreModel>> GetByGameKeyAsync(string key);
    Task<GenreModel> GetByParentIdAsync(Guid id);
    Task<GenreModel> GetByGenreName(string name);
}