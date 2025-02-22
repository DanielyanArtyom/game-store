using GameStore.Business.Exceptions;
using GameStore.Business.Mapping;

namespace GameStore.Tests.GenreServiceTests;

public class GenreServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IVisitor> _visitor;
    private readonly IMapper _mapper;

    private readonly IGenreService _genreService;

    public GenreServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _visitor = new Mock<IVisitor>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EntitiesToModelMapping()));
        _mapper = new Mapper(mapperConfig);

        _genreService = new GenreService(_unitOfWorkMock.Object, _visitor.Object, _mapper);
    }

    #region Test Data

    private Genre ValidGenreA = new Genre
    {
        Id = Guid.NewGuid(),
        Name = "Genre A",
    };

    private Genre ValidGenreB = new Genre
    {
        Id = Guid.NewGuid(),
        Name = "Genre B",
    };

    private GenreModel ExisitingGenre = new GenreModel { 
        Id = Guid.NewGuid(),
        Name = "Genre A"
    };

    #endregion

    #region GetTests

    [Fact]
    public async Task GetAllAsync_ReturnsMappedGenreModel()
    {
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 2,
            Results = new List<Genre> { ValidGenreA, ValidGenreB }
        };

        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _genreService.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResult.TotalCount, result.Count);

        _unitOfWorkMock.Verify(uow => uow.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_GenreExists_ReturnsGenreModel()
    {
        var genre = ValidGenreA;
        
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 1,
            Results = new List<Genre> { genre }
        };

        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _genreService.GetByIdAsync(genre.Id);

        Assert.NotNull(result);
        Assert.Equal(genre.Id, result.Id);

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_GenreDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 0,
            Results = new List<Genre>(),
        };

        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _genreService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Genre does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
    
    #region  ModificationTests

    [Fact]
    public async Task CreateAsync_WhenGenreAlreadyExists_ThrowsDuplicateFoundException()
    {
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 1,
            Results = new List<Genre> { ValidGenreA }
        };
        
        var cancellationToken = new CancellationToken();
        
        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _genreService.CreateAsync(ExisitingGenre, cancellationToken));
        Assert.Equal("Genre is Already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Genres.Add(It.IsAny<Genre>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenGenreDoesNotExist_CreatesNewGenre()
    {
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 0,
            Results = new List<Genre>()
        };
        
        var cancellationToken = new CancellationToken();
        
        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Genres.Add(It.IsAny<Genre>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _genreService.CreateAsync(ExisitingGenre, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Genres.Add(It.IsAny<Genre>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenGenreAlreadyExists_ThrowsDuplicateFoundException()
    {
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 1,
            Results = new List<Genre> { ValidGenreA }
        };
        
        var cancellationToken = new CancellationToken();
        
        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _genreService.UpdateAsync(ExisitingGenre, cancellationToken));
        Assert.Equal("Genre with this name already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Genres.Update(ValidGenreA.Id,It.IsAny<Genre>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_WhenGenreDoesNotExist_UpdatesExistingGenre()
    {
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 0,
            Results = new List<Genre>()
        };
        
        var cancellationToken = new CancellationToken();
        
        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Genres.Update(ValidGenreA.Id, It.IsAny<Genre>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _genreService.UpdateAsync(ExisitingGenre, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Genres.Update(ValidGenreA.Id, It.IsAny<Genre>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_GenreDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 0,
            Results = new List<Genre>()
        };

        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _genreService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Genre does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_GenreDoesExist_GenreIsRemoved()
    {
        var genre = ValidGenreA;
        
        var expectedResult = new SearchResult<Genre>
        {
            TotalCount = 1,
            Results = new List<Genre> { genre }
        };
        
        var cancellationToken = new CancellationToken();
        _unitOfWorkMock.Setup(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _genreService.DeleteAsync(genre.Id, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Genres.SearchAsync(It.IsAny<SearchContext<Genre>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Genres.Delete(It.IsAny<Genre>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    #endregion
}