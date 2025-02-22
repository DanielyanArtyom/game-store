using GameStore.Mongo.Data.Context.Entity;
using Order = GameStore.Data.Context.Entity.Order;

namespace GameStore.Business.Mapping;

public sealed class EntitiesToModelMapping : Profile
{
    public EntitiesToModelMapping()
    {
        CreateMap<GameModel, Game>()
            .ForMember(dest => dest.GameGenres,
                opt => opt.MapFrom(src => src.Genres.Select(g => new GameGenre { GenreId = g.Id })))
            .ForMember(dest => dest.GamePlatforms,
                opt => opt.MapFrom(src => src.Platforms.Select(p => new GamePlatform { PlatformId = p.Id })))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key ?? src.Name))
            .ForMember(dest => dest.Publisher, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Publisher, PublisherModel>().ReverseMap();
        CreateMap<Genre, GenreModel>().ReverseMap();
        CreateMap<PlatformModel, Platform>().ReverseMap();
        
        CreateMap<Order, OrderModel>().ReverseMap();
        CreateMap<OrderGame, OrderGameModel>().ReverseMap();
        CreateMap<PaymentMethod, PaymentMethodModel>().ReverseMap();

        CreateMap<Comment, CommentModel>().ReverseMap();
        
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<Role, RoleModel>().ReverseMap();
        CreateMap<Permission, PermissionModel>().ReverseMap();

        CreateMap<GameGenre, GameModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GameId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game.Name))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Game.Key))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game.Description));

        CreateMap<GamePlatform, GameModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GameId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game.Name))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Game.Key))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game.Description));

        CreateMap<GameGenre, GenreModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GenreId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.ParentGenreId, opt => opt.MapFrom(src => src.Genre.ParentGenreId));

        CreateMap<GamePlatform, PlatformModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PlatformId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Platform.Type));

        CreateMap<Shipper, ShipperModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ShipperID));
    }
}