using CommonBlocks.Exceptions.Handler;
using Discount.Grpc;
using CommonBlocks.Messaging.MassTransite;

var builder = WebApplication.CreateBuilder(args);
//Add Service Registration

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")!);
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetValue<string>("DatabaseSettings:Redis");
});

builder.Services.Decorate<IBasketRepository, BasketCacheRepository>();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option =>
{
    option.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")!);
});

builder.Services.AddMessageBroker(builder.Configuration);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
