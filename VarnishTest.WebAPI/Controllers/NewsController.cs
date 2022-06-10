using Ertis.Core.Collections;
using Ertis.Extensions.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using VarnishTest.Abstractions.Services;

namespace VarnishTest.WebAPI.Controllers;

[ApiController]
[Route("news")]
public class NewsController : ControllerBase
{
	#region Services

	private readonly INewsService _newsService;
	private readonly ILogger<NewsController> _logger;

	#endregion
	
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="newsService"></param>
	/// <param name="logger"></param>
	public NewsController(INewsService newsService, ILogger<NewsController> logger)
	{
		this._newsService = newsService;
		this._logger = logger;
	}

	#endregion
	
	#region Methods

	[HttpGet("{id}")]
	public async Task<IActionResult> Get([FromRoute] string id, [FromQuery] int wait)
	{
		this._logger.Log(LogLevel.Information, $"{nameof(NewsController)}/Get/{id}");

		if (wait > 0)
		{
			await Task.Delay(TimeSpan.FromSeconds(wait));
		}
		
		var news = await this._newsService.GetAsync(id);
		return news != null ? this.Ok(news) : this.NotFound(id);
	}
	
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] int wait)
	{
		this._logger.Log(LogLevel.Information, $"{nameof(NewsController)}/Get");
		
		if (wait > 0)
		{
			await Task.Delay(TimeSpan.FromSeconds(wait));
		}
		
		this.ExtractPaginationParameters(out int? skip, out int? limit, out bool withCount);
		this.ExtractSortingParameters(out string orderBy, out SortDirection? sortDirection);
				
		var newsList = await this._newsService.GetAsync(skip, limit, withCount, orderBy, sortDirection);
		return this.Ok(newsList);
	}

	#endregion
}