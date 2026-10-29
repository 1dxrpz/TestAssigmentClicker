using Assets.Controllers;
using Assets.Controllers.Tabs;
using Assets.Services;
using Assets.Services.Interfaces;
using UnityEngine;
using Zenject;
using static UIParticle;

namespace Assets.Installers
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		UIParticle ParticlePrefab;

		public override void InstallBindings()
		{
			new ServiceBinder(Container)

				// service bind chain
				.Bind<IRequestService, RequestService>()

				// controllers bind chain
				.BindController<TabController>()
				.BindController<ForecastTabController>()
				.BindController<DogBreedsTabController>()
			;

			Container.BindFactory<UIParticle, UIParticleFactory>()
				.FromComponentInNewPrefab(ParticlePrefab);
		}
	}
}