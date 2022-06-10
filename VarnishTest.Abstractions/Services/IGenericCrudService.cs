using Ertis.Core.Collections;

namespace VarnishTest.Abstractions.Services;

public interface IGenericCrudService<T>
{
	#region Methods

	T? Get(string id);
		
	ValueTask<T?> GetAsync(string id);

	IPaginationCollection<T> Get(int? skip = null, int? limit = null, bool withCount = false, string orderBy = null, SortDirection? sortDirection = null);
		
	ValueTask<IPaginationCollection<T>> GetAsync(int? skip = null, int? limit = null, bool withCount = false, string orderBy = null, SortDirection? sortDirection = null);

	#endregion
}