using VarnishTest.Core.Models;
using VarnishTest.Dto.Models;
using VarnishTest.Infrastructure.Mapping.Impls;

namespace VarnishTest.Infrastructure.Mapping;

public sealed class Mapper : IMapper
{
	#region Singleton

		private static Mapper self;

		public static Mapper Current
		{
			get
			{
				if (self == null)
					self = new Mapper();

				return self;
			}
		}

		#endregion

		#region Properties

		private IMapper Implementation { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		private Mapper()
		{
			var mappings = new MappingCollection();
			
			mappings.Add<News, NewsDto>();
			mappings.Add<NewsDto, News>();

			this.Implementation = new TinyMapperImpl(mappings);
		}

		#endregion
		
		#region Methods

		public TOut Map<TIn, TOut>(TIn instance)
		{
			if (instance == null)
			{
				return default;
			}
			
			return this.Implementation.Map<TIn, TOut>(instance);
		}
		
		public IEnumerable<TOut> MapCollection<TIn, TOut>(IEnumerable<TIn> collection)
		{
			if (collection != null)
			{
				foreach (var instance in collection)
				{
					yield return this.Implementation.Map<TIn, TOut>(instance);
				}
			}
		}

		#endregion
}