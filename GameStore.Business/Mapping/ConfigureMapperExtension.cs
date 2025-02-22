namespace GameStore.Business.Mapping;

public static class ConfigureMapperExtension
{
    public static IServiceCollection ConfigureMapper(this IServiceCollection builder)
    {
        builder.AddAutoMapper(typeof(EntitiesToModelMapping).Assembly);
        builder.AddAutoMapper(typeof(DocumentsToModelAndEntityMapping).Assembly);

        return builder;
    }
}