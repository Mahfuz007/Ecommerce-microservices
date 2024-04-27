using CommonBlocks.Behavior;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
//Add services
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")!);
}).UseLightweightSessions();

var app = builder.Build();
app.MapCarter();
app.Run();
