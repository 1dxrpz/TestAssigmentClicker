using Assets.Controllers.Tabs;
using Assets.Services;
using Assets.Services.Interfaces;
using Zenject;

namespace Assets.Installers
{
	public class SceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			new ServiceBinder(Container)

				// service bind chain
				.Bind<IRequestService, RequestService>()

				// controllers bind chain
				.BindController<ForecastTabController>()
				.BindController<DogBreedsTabController>()
				.BindController<ClickerTabController>()
			;
		}
	}
}