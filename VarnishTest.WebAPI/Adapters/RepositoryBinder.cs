using Ertis.Data.Repository;

namespace VarnishTest.WebAPI.Adapters;

public class RepositoryBinder : IRepositoryActionBinder
{
	public TEntity BeforeInsert<TEntity>(TEntity entity)
	{
		return entity;
	}

	public TEntity AfterInsert<TEntity>(TEntity entity)
	{
		return entity;
	}

	public TEntity BeforeUpdate<TEntity>(TEntity entity)
	{
		return entity;
	}

	public TEntity AfterUpdate<TEntity>(TEntity entity)
	{
		return entity;
	}
}