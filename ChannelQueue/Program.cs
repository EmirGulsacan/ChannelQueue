using ChannelQueue.Models;
using ChannelQueue.Services;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Configure ChannelSettings
builder.Services.Configure<ChannelSettings>(builder.Configuration.GetSection("ChannelSettings"));

// Add services to the container.
builder.Services.AddControllers();

// Configure Channel based on settings
var capacity = builder.Configuration.GetValue<int>("ChannelSettings:Capacity");
var channel = Channel.CreateBounded<string>(capacity);
builder.Services.AddSingleton(channel);
builder.Services.AddSingleton(channel.Writer);
builder.Services.AddSingleton(channel.Reader);
builder.Services.AddSingleton<Producer>();
builder.Services.AddHostedService<Consumer>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<Consumer>>();
    var channelReader = sp.GetRequiredService<ChannelReader<string>>();
    return new Consumer(channelReader, logger, capacity);
});

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
