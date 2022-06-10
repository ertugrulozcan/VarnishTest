using Ertis.Data.Repository;
using Ertis.MongoDB.Configuration;
using Ertis.MongoDB.Repository;
using VarnishTest.Dao.Repositories.Interfaces;
using VarnishTest.Dto.Models;

namespace VarnishTest.Dao.Repositories;

public class NewsRepository : MongoRepositoryBase<NewsDto>, INewsRepository
{
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="settings"></param>
	/// <param name="actionBinder"></param>
	public NewsRepository(IDatabaseSettings settings, IRepositoryActionBinder actionBinder) : base(settings, "news", actionBinder)
	{
			
	}

	#endregion
}