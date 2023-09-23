using System.Reflection;
using LlveTixTest.CoverAlbums;
using LlveTixTest.CoverAlbums.PlaceHolder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddHttpClient<IPlaceHolderService, PlaceHolderService>((serviceProvider, client) =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.AddCoverAlbumsEndpoints();
app.Run();
