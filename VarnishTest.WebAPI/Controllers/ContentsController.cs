using Ertis.Core.Collections;
using Ertis.Extensions.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using VarnishTest.Abstractions.Services;

namespace VarnishTest.WebAPI.Controllers;

[ApiController]
[Route("contents")]
public class ContentsController : ControllerBase
{
	#region Services

	private readonly IContentService _contentService;
	private readonly ILogger<NewsController> _logger;

	#endregion
	
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="contentService"></param>
	/// <param name="logger"></param>
	public ContentsController(IContentService contentService, ILogger<NewsController> logger)
	{
		this._contentService = contentService;
		this._logger = logger;
	}

	#endregion
	
	#region Methods
	
	[HttpGet("on")]
	public IActionResult On()
	{
		this._logger.Log(LogLevel.Information, "/contents/on");
		this._contentService.On();
		return this.Ok("Contents endpoint re-activated");
	}
	
	[HttpGet("off")]
	public IActionResult Off()
	{
		this._logger.Log(LogLevel.Information, "/contents/off");
		this._contentService.Off();
		return this.Ok("Contents endpoint disabled");
	}

	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] int wait)
	{
		this._logger.Log(LogLevel.Information, "/contents");
		
		if (wait > 0)
		{
			await Task.Delay(TimeSpan.FromSeconds(wait));
		}
		
		this.ExtractPaginationParameters(out int? skip, out int? limit, out bool withCount);
		this.ExtractSortingParameters(out string orderBy, out SortDirection? sortDirection);
				
		var newsList = await this._contentService.GetAsync(skip, limit, withCount, orderBy, sortDirection);
		return this.Ok(newsList);
	}
	
	#endregion
}