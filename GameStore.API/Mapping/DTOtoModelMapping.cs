using PermissionDto = GameStore.API.DTO.Responses.PermissionDto;

namespace GameStore.API.Mapping;

public class DTOtoModelMapping : Profile
{
    public DTOtoModelMapping()
    {
        CreateMap<GameCreateRequest, GameModel>()
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.Genres.Select(genreId => new GenreModel { Id = genreId })))
            .ForMember(dest => dest.Platforms,
                opt => opt.MapFrom(src => src.Platforms.Select(platformId => new PlatformModel { Id = platformId })));

        CreateMap<GameUpdateRequest, GameModel>()
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.Genres.Select(genreId => new GenreModel { Id = genreId })))
            .ForMember(dest => dest.Platforms,
                opt => opt.MapFrom(src => src.Platforms.Select(platformId => new PlatformModel { Id = platformId })));

        CreateMap<GameModel, GameDto>();
        CreateMap<GameSearchFilters, GameSearchFilterModel>();
        CreateMap<PagedGameModel, PagedGamesDto>();

        CreateMap<LoginRequest, UserModel>();
        CreateMap<AuthorizationModel, AuthorizationDto>();
        CreateMap<RegisterRequest, UserModel>();
        CreateMap<CheckAccessRequest, CheckAccessModel>();
        CreateMap<UserModel, UserDto>();

        CreateMap<PermissionModel, PermissionDto>().ReverseMap();
        CreateMap<RoleCreateRequest, RoleModel>();
        
        CreateMap<UserUpdateRequest, UserModel>()
            .ForMember(dest => dest.Roles,
                opt => opt.MapFrom(src => src.Roles.Select(genreId => new RoleModel { Id = genreId })));

        CreateMap<RoleModel, RoleDto>();
        
        CreateMap<AddCommentRequest, CommentModel>();
        CreateMap<CommentModel, CommentDto>();
        CreateMap<UserBanRequest, UserBanModel>();
        
        CreateMap<OrderModel, OrderDto>().ReverseMap();
        CreateMap<OrderGameModel, OrderGameDto>().ReverseMap();
        CreateMap<PaymentMethodModel, PaymentMethodDto>().ReverseMap();
        
        CreateMap<GenreCreateRequest, GenreModel>();
        CreateMap<GenreUpdateRequest, GenreModel>();
        CreateMap<GenreModel, GenreDto>();
        
        CreateMap<PlatformCreateRequest, PlatformModel>();
        CreateMap<PlatformUpdateRequest, PlatformModel>();
        CreateMap<PlatformModel, PlatformDto>();

        CreateMap<PublisherModel, PublisherDto>();
        CreateMap<PublisherCreateRequest, PublisherModel>();
        CreateMap<PublisherUpdateRequest, PublisherModel>();

        CreateMap<ShipperModel, ShipperDto>();
    }
}