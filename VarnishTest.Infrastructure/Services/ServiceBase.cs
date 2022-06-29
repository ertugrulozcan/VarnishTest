using Ertis.Data.Models;
using Ertis.MongoDB.Repository;
using VarnishTest.Core.Models;

namespace VarnishTest.Infrastructure.Services;

public abstract class ServiceBase<TModel, TDto> : GenericCrudService<TModel, TDto> where TModel : IHasIdentifier where TDto : IEntity<string>
{
	#region Properties

	public bool IsActive { get; private set; } = true;

	#endregion
	
	#region Constructors

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="repository"></param>
	protected ServiceBase(IMongoRepository<TDto> repository) : base(repository)
	{
			
	}

	#endregion

	#region Methods

	public void On()
	{
		this.IsActive = true;
	}

	public void Off()
	{
		this.IsActive = false;
	}

	#endregion
}