namespace VarnishTest.Infrastructure.Mapping;

public interface IMapper
{
	TOut Map<TIn, TOut>(TIn instance);
}