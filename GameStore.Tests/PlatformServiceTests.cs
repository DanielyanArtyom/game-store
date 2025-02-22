using GameStore.Business.Exceptions;
using GameStore.Business.Mapping;

namespace GameStore.Tests.PlatformServiceTests;

public class PlatformServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IVisitor> _visitor;
    private readonly IMapper _mapper;

    private readonly IPlatformService _platformService;

    public PlatformServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _visitor = new Mock<IVisitor>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EntitiesToModelMapping()));
        _mapper = new Mapper(mapperConfig);

        _platformService = new PlatformService( _unitOfWorkMock.Object, _visitor.Object, _mapper);
    }

    #region Test Data

    private Platform ValidPlatformA = new Platform()
    {
        Id = Guid.NewGuid(),
        Type = "Platform A",
    };

    private Platform ValidPlatformB = new Platform
    {
        Id = Guid.NewGuid(),
        Type = "Platform B",
    };

    private PlatformModel ExisingPlatformModel = new PlatformModel
    {
        Id = Guid.NewGuid(),
        Type = "Platform A",
    };
    
    #endregion

    #region GetTests

     [Fact]
    public async Task GetAllAsync_ReturnsMappedPlatformModel()
    {
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 2,
            Results = new List<Platform> { ValidPlatformA, ValidPlatformB }
        };

        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _platformService.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResult.TotalCount, result.Count);

        _unitOfWorkMock.Verify(uow => uow.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_PlatformExists_ReturnsPlatformModel()
    {
        var platform = ValidPlatformA;
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 1,
            Results = new List<Platform> { platform }
        };

        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _platformService.GetByIdAsync(platform.Id);

        Assert.NotNull(result);
        Assert.Equal(platform.Id, result.Id);

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_PlatformDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 0,
            Results = new List<Platform>()
        };

        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _platformService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Platform does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
   
    #region  ModificationTests

    [Fact]
    public async Task CreateAsync_WhenPlatformAlreadyExists_ThrowsDuplicateFoundException()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 1,
            Results = new List<Platform> { ValidPlatformA }
        };
        
        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _platformService.CreateAsync(ExisingPlatformModel, cancellationToken));
        Assert.Equal("Platform is Already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Platforms.Add(It.IsAny<Platform>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenPlatformDoesNotExist_CreatesNewPlatform()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 0,
            Results = new List<Platform>()
        };
        
        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Platforms.Add(It.IsAny<Platform>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _platformService.CreateAsync(ExisingPlatformModel, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Platforms.Add(It.IsAny<Platform>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenPlatformAlreadyExists_ThrowsDuplicateFoundException()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 1,
            Results = new List<Platform> { ValidPlatformA }
        };
        
        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _platformService.UpdateAsync(ExisingPlatformModel, cancellationToken));
        Assert.Equal("Platform with this name already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Platforms.Update(ValidPlatformA.Id, It.IsAny<Platform>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_WhenPlatformDoesNotExist_UpdatesExistingPlatform()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 0,
            Results = new List<Platform>()
        };
        
        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Platforms.Update(ValidPlatformA.Id, It.IsAny<Platform>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _platformService.UpdateAsync(ExisingPlatformModel, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Platforms.Update(ValidPlatformA.Id, It.IsAny<Platform>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_PlatformDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 0,
            Results = new List<Platform>()
        };

        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _platformService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Platform does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_PlatformDoesExist_PlatformIsRemoved()
    {
        var platform = ValidPlatformA;
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Platform>
        {
            TotalCount = 1,
            Results = new List<Platform>{ platform }
        };
        
        _unitOfWorkMock.Setup(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _platformService.DeleteAsync(platform.Id, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Platforms.SearchAsync(It.IsAny<SearchContext<Platform>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Platforms.Delete(It.IsAny<Platform>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    #endregion
}