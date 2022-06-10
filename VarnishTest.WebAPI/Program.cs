using Ertis.Data.Repository;
using Ertis.MongoDB.Configuration;
using Ertis.MongoDB.Database;
using Microsoft.Extensions.Options;
using VarnishTest.Abstractions.Services;
using VarnishTest.Dao.Repositories;
using VarnishTest.Dao.Repositories.Interfaces;
using VarnishTest.Infrastructure.Services;
using VarnishTest.WebAPI.Adapters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
builder.Services.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoDatabase, MongoDatabase>();
builder.Services.AddSingleton<INewsRepository, NewsRepository>();
builder.Services.AddSingleton<IRepositoryActionBinder, RepositoryBinder>();
builder.Services.AddSingleton<INewsService, NewsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();