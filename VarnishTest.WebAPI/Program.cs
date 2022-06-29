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
builder.Services.AddSingleton<IContentService, ContentService>();

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

app.Use(async (context, next) =>
{
	if (context.Request.Path.Value!.StartsWith("/news"))
	{
		if (!context.Request.Path.Value!.StartsWith("/news/on") && !context.Request.Path.Value!.StartsWith("/news/off"))
		{
			var newsService = context.RequestServices.GetRequiredService<INewsService>();
			if (!newsService.IsActive)
			{
				context.Response.HttpContext.Abort();	
			}	
		}
	}
	
	if (context.Request.Path.Value!.StartsWith("/contents"))
	{
		if (!context.Request.Path.Value!.StartsWith("/contents/on") && !context.Request.Path.Value!.StartsWith("/contents/off"))
		{
			var contentService = context.RequestServices.GetRequiredService<IContentService>();
			if (!contentService.IsActive)
			{
				context.Response.HttpContext.Abort();	
			}	
		}
	}
	
	await next.Invoke();
});

app.Run();