using GameStore.Mongo.Data.Context.Entity;
using MongoOrder = GameStore.Mongo.Data.Context.Entity.Order;
using Order = GameStore.Data.Context.Entity.Order;

namespace GameStore.Business.Mapping;

public class DocumentsToModelAndEntityMapping: Profile
{
    public DocumentsToModelAndEntityMapping()
    {
        CreateMap<Shipper, ShipperModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ShipperID))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty));

        CreateMap<Supplier, Publisher>();

        CreateMap<Category, Genre>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty))
            .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.CategoryID));

        CreateMap<MongoOrder, Order>()
            .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.OrderID))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.OrderDate)))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src =>
                    DateTime.Parse(src.OrderDate) > DateTime.UtcNow ? OrderStatus.Open : OrderStatus.Paid))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty));

        CreateMap<MongoOrder, OrderModel>()
            .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.OrderID))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.OrderDate)))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src =>
                    DateTime.Parse(src.OrderDate) > DateTime.UtcNow ? OrderStatus.Open : OrderStatus.Paid))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => Guid.Empty));

        CreateMap<Product, Game>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.UnitInStock, opt => opt.MapFrom(src => src.UnitsInStock))
            .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty));
        
        CreateMap<Product, GameModel>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.UnitInStock, opt => opt.MapFrom(src => src.UnitsInStock))
            .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty));
    }
}