using VarnishTest.Abstractions.Services;
using VarnishTest.Core.Models;
using VarnishTest.Dao.Repositories.Interfaces;
using VarnishTest.Dto.Models;

namespace VarnishTest.Infrastructure.Services;

public class NewsService : GenericCrudService<News, NewsDto>, INewsService
{
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="newsRepository"></param>
	public NewsService(INewsRepository newsRepository) : base(newsRepository)
	{
			
	}

	#endregion
}