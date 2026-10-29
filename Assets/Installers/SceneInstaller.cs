using Assets.Controllers.Tabs;
using Assets.Services;
using Assets.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Installers
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		GameObject ParticlePrefab;

		public override void InstallBindings()
		{
			new ServiceBinder(Container)

				// service bind chain
				.Bind<IRequestService, RequestService>()

				// controllers bind chain
				.BindController<ForecastTabController>()
				.BindController<DogBreedsTabController>()
			;

			Container.BindFactory<Particles, Particles.Factory>()
				 .FromComponentInNewPrefab(ParticlePrefab)
				 .UnderTransformGroup("SpawnedParticles");
		}
	}
}