using VarnishTest.Abstractions.Services;
using VarnishTest.Core.Models;
using VarnishTest.Dao.Repositories.Interfaces;
using VarnishTest.Dto.Models;

namespace VarnishTest.Infrastructure.Services;

public class ContentService : ServiceBase<News, NewsDto>, IContentService
{
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="newsRepository"></param>
	public ContentService(INewsRepository newsRepository) : base(newsRepository)
	{
			
	}

	#endregion
}