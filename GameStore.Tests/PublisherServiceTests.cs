using GameStore.Business.Exceptions;
using GameStore.Business.Mapping;

namespace GameStore.Tests;

public class PublisherServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IVisitor> _visitor;
    private readonly IMapper _mapper;

    private readonly IPublisherService _publisherService;

    public PublisherServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _visitor = new Mock<IVisitor>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EntitiesToModelMapping()));
        _mapper = new Mapper(mapperConfig);

        _publisherService = new PublisherService( _unitOfWorkMock.Object, _visitor.Object, _mapper);
    }
    
    #region Test Data

    private Publisher ValidPublisherA = new Publisher()
    {
        Id = Guid.NewGuid(),
        CompanyName = "Publisher A",
        Description = "Publisher A Description",
        HomePage = "Home Page A"
    };
    
    private Publisher ValidPublisherB = new Publisher()
    {
        Id = Guid.NewGuid(),
        CompanyName = "Publisher B",
        Description = "Publisher B Description",
        HomePage = "Home Page B"
    };

    private PublisherModel ExistingPublisherModel = new PublisherModel
    {
        Id = Guid.NewGuid(),
        CompanyName = "Publisher A",
        Description = "Publisher A Description",
        HomePage = "Home Page A"
    };

    #endregion
    
    #region GetTests
    
    [Fact]
    public async Task GetAllAsync_ReturnsMappedPublisherModel()
    {
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 2,
            Results = new List<Publisher> { ValidPublisherA, ValidPublisherB }
        };

        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _publisherService.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResult.TotalCount, result.Count);

        _unitOfWorkMock.Verify(uow => uow.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_PlatformExists_ReturnsPublisherModel()
    {
        var publisher = ValidPublisherA;
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 1,
            Results = new List<Publisher> { publisher }
        };

        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _publisherService.GetByIdAsync(publisher.Id);

        Assert.NotNull(result);
        Assert.Equal(publisher.Id, result.Id);

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_PublisherDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 0,
            Results = new List<Publisher>()
        };

        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _publisherService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Publisher does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region  ModificationTests

    [Fact]
    public async Task CreateAsync_WhenPublisherAlreadyExists_ThrowsDuplicateFoundException()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 1,
            Results = new List<Publisher> { ValidPublisherA}
        };
        
        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _publisherService.CreateAsync(ExistingPublisherModel, cancellationToken));
        Assert.Equal("Publisher is Already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Publishers.Add(It.IsAny<Publisher>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenPublisherDoesNotExist_CreatesNewPublisher()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 0,
            Results = new List<Publisher>()
        };
        
        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Publishers.Add(It.IsAny<Publisher>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _publisherService.CreateAsync(ExistingPublisherModel, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Publishers.Add(It.IsAny<Publisher>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenPublisherAlreadyExists_ThrowsDuplicateFoundException()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 0,
            Results = new List<Publisher> { ValidPublisherA }
        };
        
        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var exception = await Assert.ThrowsAsync<DuplicateFoundException>(() => _publisherService.UpdateAsync(ExistingPublisherModel, cancellationToken));
        Assert.Equal("Publisher is Already exists", exception.Message);

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Publishers.Update(ValidPublisherA.Id, It.IsAny<Publisher>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_WhenPublisherDoesNotExist_UpdatesExistingPublisher()
    {
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 0,
            Results = new List<Publisher>()
        };
        
        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        _unitOfWorkMock.Setup(x => x.Publishers.Update(ValidPublisherA.Id, It.IsAny<Publisher>()));

        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _publisherService.UpdateAsync(ExistingPublisherModel, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Publishers.Update(ValidPublisherA.Id, It.IsAny<Publisher>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_PublisherDoesNotExist_ThrowsNotFoundException()
    {
        var notExistingGuid = Guid.NewGuid();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 0,
            Results = new List<Publisher>()
        };

        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var act = () => _publisherService.DeleteAsync(notExistingGuid);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);

        Assert.Equal("Publisher does not exists", exception.Message); 

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_PublisherDoesExist_PlatformIsRemoved()
    {
        var publisher = ValidPublisherA;
        var cancellationToken = new CancellationToken();
        
        var expectedResult = new SearchResult<Publisher>
        {
            TotalCount = 1,
            Results = new List<Publisher> { publisher }
        };
        
        _unitOfWorkMock.Setup(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        _unitOfWorkMock.Setup(x => x.CompleteAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        await _publisherService.DeleteAsync(publisher.Id, cancellationToken);

        _unitOfWorkMock.Verify(x => x.Publishers.SearchAsync(It.IsAny<SearchContext<Publisher>>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Publishers.Delete(It.IsAny<Publisher>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(cancellationToken), Times.Once);
    }
    
    #endregion
    
}