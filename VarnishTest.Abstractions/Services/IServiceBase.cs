namespace VarnishTest.Abstractions.Services;

public interface IServiceBase<T> : IGenericCrudService<T>
{
	bool IsActive { get; }

	void On();
	
	void Off();
}