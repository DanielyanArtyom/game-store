using GameStore.API.Extensions;
using GameStore.Business.DependencyInjection;
using GameStore.Business.Mapping;
using GameStore.Data.DependencyInjection;
using GameStore.Mongo.Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("x-total-numbers-of-games");
    });
});

builder.Services.AddMemoryCache();

var paymentOptions = builder.Configuration.GetSection("PaymentServiceOptions").Get<PaymentServiceOptions>();

var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnectionString")!;

builder.Services
    .AddGameStoreServices(paymentOptions)
    .AddGameStoreProvider(sqlConnectionString)
    .AddGameStoreMongoProvider(mongoConnectionString)
    .ConfigureMapper()
    .Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder
    .Configure()
    .AddLogger()
    .AddWebApi()
    .AddMapper()
    .AddSwagger()
    .AddAuthentication();

var app = builder.Build();

app.UseSerilogRequestLogging(); 

app.UseCors("AllowAll");

app.UseMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDatabaseMigration();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();