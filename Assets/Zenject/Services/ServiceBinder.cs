using Assets.Controllers.Interfaces;
using Assets.Services.Interfaces;
using Zenject;

namespace Assets.Services
{
	internal class ServiceBinder
	{
		DiContainer Container { get; }
		public ServiceBinder(DiContainer container)
		{
			Container = container;
		}

		public ServiceBinder Bind<TInterface, TService>() 
			where TService : class, TInterface, IService, new()
		{
			Container
				.Bind<TInterface>()
				.To<TService>()
				.FromNew()
				.AsSingle();

			return this;
		}

		public ServiceBinder BindController<TController>()
			where TController : IController
		{
			Container
				.Bind<TController>()
				.FromComponentInHierarchy()
				.AsSingle()
				.Lazy();

			return this;
		}
	}
}
