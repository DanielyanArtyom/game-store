using System.Linq.Expressions;

namespace GameStore.Business.Service;

public class PlatformService : IPlatformService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVisitor _visitor;

    public PlatformService(IUnitOfWork unitOfWork, IVisitor visitor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _visitor = visitor;
    }

    public async Task CreateAsync(PlatformModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingPlatform = await _unitOfWork.Platforms.SearchAsync(new SearchContext<Platform> { Filter = x => x.Type == request.Type }, ct);

        if (existingPlatform.TotalCount != 0)
        {
            throw new DuplicateFoundException("Platform is Already exists");
        }

        var platform = _mapper.Map<Platform>(request);

        _unitOfWork.Platforms.Add(platform);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task UpdateAsync(PlatformModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var existingPlatform = await _unitOfWork.Platforms.SearchAsync(new SearchContext<Platform>{ Filter = x => x.Type == request.Type && x.Id != request.Id }, ct);

        if (existingPlatform.TotalCount != 0)
        {
            throw new DuplicateFoundException("Platform with this name already exists");
        }

        var platform = _mapper.Map<Platform>(request);

        _unitOfWork.Platforms.Update(platform.Id, platform);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<PlatformModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var platform = (await _unitOfWork.Platforms.SearchAsync(new SearchContext<Platform> { Filter = x => x.Id == id }, ct)).Results.FirstOrDefault();

        return _mapper.Map<PlatformModel>(platform);
    }

    public async Task<List<PlatformModel>> GetAllAsync()
    {
        var platforms = (await _unitOfWork.Platforms.SearchAsync(new SearchContext<Platform>())).Results.ToList();

        return _mapper.Map<List<PlatformModel>>(platforms);
    }

    public async Task<List<PlatformModel>> GetByGameKeyAsync(string key)
    {
        var platforms = (await _unitOfWork.GamePlatforms.SearchAsync(new SearchContext<GamePlatform>
        {
            Filter = x => x.Game.Key == key,
            Include = new Expression<Func<GamePlatform, object>>[] { x => x.Platform }
        })).Results.ToList();

        return _mapper.Map<List<PlatformModel>>(platforms);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var platform = (await _unitOfWork.Platforms.SearchAsync(new SearchContext<Platform>{ Filter = x => x.Id == id }, ct)).Results.FirstOrDefault();

        if (platform == null)
        {
            throw new NotFoundException("Platform does not exists");
        }

        _unitOfWork.Platforms.Delete(platform);
        await _unitOfWork.CompleteAsync(ct);
    }
}