using Ertis.Core.Collections;
using Ertis.Data.Models;
using Ertis.MongoDB.Repository;
using VarnishTest.Abstractions.Services;
using VarnishTest.Infrastructure.Mapping;

namespace VarnishTest.Infrastructure.Services;

public class GenericCrudService<TModel, TDto> : 
	IGenericCrudService<TModel>
	where TModel : Core.Models.IHasIdentifier
	where TDto : IEntity<string>
{
	#region Services

	protected readonly IMongoRepository<TDto> repository;

	#endregion

	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="repository"></param>
	protected GenericCrudService(IMongoRepository<TDto> repository)
	{
		this.repository = repository;
	}

	#endregion
	
	#region Read Methods

	public TModel? Get(string id)
	{
		var dto = this.repository.FindOne(id);
		return Mapper.Current.Map<TDto, TModel>(dto);
	}
	
	public async ValueTask<TModel?> GetAsync(string id)
	{
		var dto = await this.repository.FindOneAsync(id);
		return Mapper.Current.Map<TDto, TModel>(dto);
	}
	
	public IPaginationCollection<TModel> Get(int? skip = null, int? limit = null, bool withCount = false, string orderBy = null, SortDirection? sortDirection = null)
	{
		var paginatedDtoCollection = this.repository.Find(expression: null, skip, limit, withCount, orderBy, sortDirection);
		if (paginatedDtoCollection?.Items != null)
		{
			return new PaginationCollection<TModel>
			{
				Items = paginatedDtoCollection.Items.Select(x => Mapper.Current.Map<TDto, TModel>(x)),
				Count = paginatedDtoCollection.Count
			};
		}
		else
		{
			return new PaginationCollection<TModel>();
		}
	}
	
	public async ValueTask<IPaginationCollection<TModel>> GetAsync(int? skip = null, int? limit = null, bool withCount = false, string orderBy = null, SortDirection? sortDirection = null)
	{
		var paginatedDtoCollection = await this.repository.FindAsync(expression: null, skip, limit, withCount, orderBy, sortDirection);
		if (paginatedDtoCollection?.Items != null)
		{
			return new PaginationCollection<TModel>
			{
				Items = paginatedDtoCollection.Items.Select(x => Mapper.Current.Map<TDto, TModel>(x)),
				Count = paginatedDtoCollection.Count
			};
		}
		else
		{
			return new PaginationCollection<TModel>();
		}
	}

	#endregion
}