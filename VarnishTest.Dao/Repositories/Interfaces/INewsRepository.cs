using Ertis.MongoDB.Repository;
using VarnishTest.Dto.Models;

namespace VarnishTest.Dao.Repositories.Interfaces;

public interface INewsRepository : IMongoRepository<NewsDto>
{
	
}