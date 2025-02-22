namespace GameStore.Business.Service;

public class GenreService : IGenreService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVisitor _visitor;

    public GenreService(IUnitOfWork unitOfWork,  IVisitor visitor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _visitor = visitor;
    }

    public async Task CreateAsync(GenreModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingGenre = await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.Name == request.Name }, ct);

        if (existingGenre.TotalCount != 0)
        {
            throw new DuplicateFoundException("Genre is Already exists");
        }

        if (request.ParentGenreId.HasValue)
        {
            var isParentExists = (await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre>{ Filter = x => x.Id == request.ParentGenreId }, ct)).Results.FirstOrDefault();

            if (isParentExists == null)
            {
                throw new NotFoundException("Parent Genre is not exists");
            }
        }

        var genre = _mapper.Map<Genre>(request);

        _unitOfWork.Genres.Add(genre);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task UpdateAsync(GenreModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingGenre = await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.Name == request.Name && x.Id != request.Id }, ct);

        if (existingGenre.TotalCount != 0)
        {
            throw new DuplicateFoundException("Genre with this name already exists");
        }

        if (request.ParentGenreId.HasValue)
        {
            var isParentExists = await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.Id == request.ParentGenreId }, ct);

            if (isParentExists.TotalCount == 0)
            {
                throw new NotFoundException("Parent Genre is not exists");
            }
        }

        var genre = _mapper.Map<Genre>(request);

        _unitOfWork.Genres.Update(genre.Id, genre);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<GenreModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var genre = (await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.Id == id }, ct)).Results.FirstOrDefault();

        return _mapper.Map<GenreModel>(genre);
    }

    public async Task<GenreModel> GetByParentIdAsync(Guid id)
    {
        var genre = (await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.ParentGenreId == id })).Results.FirstOrDefault();

        return _mapper.Map<GenreModel>(genre);
    }

    public async Task<GenreModel> GetByGenreName(string name)
    {
        var genre = (await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.Name == name })).Results.FirstOrDefault();

        return _mapper.Map<GenreModel>(genre);
    }

    public async Task<List<GenreModel>> GetAllAsync()
    {
        var genres = (await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre>())).Results.ToList();

        return _mapper.Map<List<GenreModel>>(genres);
    }

    public async Task<List<GenreModel>> GetByGameKeyAsync(string key)
    {
        var genres = (await _unitOfWork.GameGenres.SearchAsync(new SearchContext<GameGenre>
        {
            Filter = x => x.Game.Key == key,
            Include = new Expression<Func<GameGenre, object>>[] { x => x.Genre }
        })).Results.ToList();

        return _mapper.Map<List<GenreModel>>(genres);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var genre = (await _unitOfWork.Genres.SearchAsync(new SearchContext<Genre> { Filter = x => x.Id == id }, ct)).Results.FirstOrDefault();

        if (genre == null)
        {
            throw new NotFoundException("Genre does not exists");
        }

        _unitOfWork.Genres.Delete(genre);
        await _unitOfWork.CompleteAsync(ct);
    }
}