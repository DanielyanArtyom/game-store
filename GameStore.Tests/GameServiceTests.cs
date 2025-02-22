using GameStore.Business.DataManager.Interface;
using GameStore.Business.Exceptions;
using GameStore.Business.Mapping;

namespace GameStore.Tests.GameServiceTest;

public class GameServiceTests
{
    private readonly Mock<IFileService> _mockFileService;

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly Mock<IVisitor> _visitor;
    private readonly GameService _gameService;
    private readonly Mock<IGamesManager> _gameManagerMock;
    
    public GameServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _visitor = new Mock<IVisitor>();
        _mockFileService = new Mock<IFileService>();
        _gameManagerMock = new Mock<IGamesManager>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EntitiesToModelMapping()));
        _mapper = new Mapper(mapperConfig);

        _gameService = new GameService(_gameManagerMock.Object, _mapper ,_visitor.Object);
    }
    
    #region Test Data

      private Game ValidGameA = new Game
    {
        Id = Guid.NewGuid(),
        Name = "Game A",
        Key = "Key A",
        Description = "Description A",
        Price = 14,
        Discount = 12,
        UnitInStock = 5,
        GamePlatforms = new List<GamePlatform> { new GamePlatform() { PlatformId = new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa") } },
        GameGenres = new List<GameGenre> { new GameGenre { GenreId = new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc") } },
        PublisherId = new Guid("02e73b48-bf3e-45df-a107-c65f302ce8de"),
        Views = 10,
        PublishDate = DateTime.UtcNow,
        OriginalId = 0,
        ReorderLevel = 1,
        Discontinued = false,
        QuantityPerUnit = "10x",
        UnitsOnOrder = 2,
    };
    
    private Game ValidGameB = new Game
    {
        Id = Guid.NewGuid(),
        Name = "Game B",
        Key = "Key B",
        Description = "Description B",
        Price = 14,
        Discount = 12,
        UnitInStock = 5,
        GamePlatforms = new List<GamePlatform> { new GamePlatform() { PlatformId = new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa") } },
        GameGenres = new List<GameGenre> { new GameGenre { GenreId = new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc") } },
        PublisherId = new Guid(),
        Views = 12,
        PublishDate = DateTime.UtcNow,
        OriginalId = 3,
        ReorderLevel = 2,
        Discontinued = true,
        QuantityPerUnit = "20x",
        UnitsOnOrder = 1,
    };
    
    private GameModel ExistingGameModel = new GameModel { 
        Id = Guid.NewGuid(),
        Name = "Game A",
        Key = "Key A",
        Description = "Description A",
        Price = 14,
        Discount = 12,
        UnitInStock = 5,
        Genres = new List<Genre>{ new Genre {Id = new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc"), Name = "Genre A"}},
        Platforms = new List<Platform>{ new Platform {Id = new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa") , Type = "Platform A"} },
        PublisherId = new Guid("02e73b48-bf3e-45df-a107-c65f302ce8de"),
        Views = 22,
        PublishDate = DateTime.UtcNow,
        OriginalId = 3,
        ReorderLevel = 2,
        Discontinued = true,
        QuantityPerUnit = "20x",
        UnitsOnOrder = 1,
    };


    #endregion

    #region GetTests

    [Fact]
    public async Task GetAllAsync_ReturnsMappedGameModel()
    {
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 2,
            Results = new List<Game> { ValidGameA, ValidGameB }
        };

        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _gameService.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResult.TotalCount, result.Count);

        _unitOfWorkMock.Verify(uow => uow.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_GameExists_ReturnsGameModel()
    {
        var game = ValidGameA;
        
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 1,
            Results = new List<Game> { game }
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _gameService.GetByIdAsync(game.Id);

        Assert.NotNull(result);
        Assert.Equal(game.Id, result.Id);

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task GetByKeyAsync_GameExists_ReturnsGameModel()
    {
        var game = ValidGameA;
        
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 1,
            Results = new List<Game> { game }
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _gameService.GetByKeyAsync(game.Key);

        Assert.NotNull(result);
        Assert.Equal(game.Key, result.Key);

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task GetByIdAsync_GameDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 0,
            Results = new List<Game>()
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        var act = () => _gameService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Game does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

 
    [Fact]
    public async Task GetFileBytesAsync_GameExists_ReturnsFileBytes()
    {
        var game = ValidGameA;
        var fileBytes = new byte[] { 0x01, 0x02, 0x03 };

        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 1,
            Results = new List<Game> { game }
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _mockFileService.Setup(x => x.GenerateFileBytes(It.IsAny<Game>()))
            .ReturnsAsync(fileBytes);

        var result = await _gameService.DownladGameFileAsync(game.Key);

        Assert.Equal(fileBytes, result);
        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockFileService.Verify(x => x.GenerateFileBytes(It.IsAny<Game>()), Times.Once);
    }
    
    [Fact]
    public async Task GetFileBytesAsync_GameDoesNotExist_ThrowsNotFoundException()
    {
        var notExisitngKey = "randomKey";

        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 0,
            Results = new List<Game>()
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _gameService.DownladGameFileAsync(notExisitngKey);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Game is not exists", exception.Message); 
        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockFileService.Verify(x => x.GenerateFileBytes(It.IsAny<Game>()), Times.Never);
    }

    #endregion

    #region  ModificationTests

    [Fact]
    public async Task CreateAsync_WhenGameAlreadyExists_ThrowsDuplicateFoundException()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 1,
            Results = new List<Game> { ValidGameA }
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _gameService.CreateAsync(ExistingGameModel, cancellationToken));
        Assert.Equal("Game is Already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Games.Add(It.IsAny<Game>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenGameDoesNotExist_CreatesNewGame()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 0,
            Results = new List<Game>()
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Games.Add(It.IsAny<Game>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _gameService.CreateAsync(ExistingGameModel, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Games.Add(It.IsAny<Game>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_GameDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 0,
            Results = new List<Game>()
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _gameService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Game does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_GameDoesExist_GameIsRemoved()
    {
        var game = ValidGameA;
        var cancellationToken = new CancellationToken();

        var expectedResult = new SearchResult<Game>
        {
            TotalCount = 1,
            Results = new List<Game>{ game }
        };
        
        _unitOfWorkMock.Setup(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _gameService.DeleteAsync(game.Id, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Games.SearchAsync(It.IsAny<SearchContext<Game>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Games.Delete(It.IsAny<Game>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }

    #endregion
}