using Assets.Controllers;
using Assets.Controllers.Tabs;
using Assets.MVC.Views;
using Assets.Resources.ScriptableObjects;
using Assets.Services;
using Assets.Services.Interfaces;
using UnityEngine;
using Zenject;
using static Assets.MVC.Views.DogBreedView;
using static UIParticle;

namespace Assets.Installers
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		PrefabRegistry PrefabRegistry;

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
				.FromComponentInNewPrefab(PrefabRegistry.ParticlePrefab);

			Container.BindFactory<DogBreedView, DogBreedViewFactory>()
				.FromComponentInNewPrefab(PrefabRegistry.DogBreedViewPrefab);
		}
	}
}