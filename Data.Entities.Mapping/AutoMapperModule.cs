using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace Data.Entities.Mapping
{
	public class AutoMapperModule : ServiceModuleBase
	{
		public override void RegisterServices(IServiceCollection serviceCollection)
		{
			var configuration = new MapperConfiguration(expression => expression.AddProfile(new AutoMapperProfile()));
			ProjectionExtensions.Init(configuration);
		}
	}

	public static class ProjectionExtensions
	{
		private static MapperConfiguration _mapperConfiguration;

		public static void Init(MapperConfiguration mapperConfiguration)
		{
			_mapperConfiguration = mapperConfiguration;
		}

		public static IQueryable<TTo> Project<TFrom, TTo>(this IQueryable<TFrom> fromQueryable)
		{
			return fromQueryable.ProjectTo<TTo>(_mapperConfiguration);
		}
	}
}