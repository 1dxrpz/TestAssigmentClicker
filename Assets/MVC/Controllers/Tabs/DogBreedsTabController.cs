using Assets.Controllers.Interfaces;
using Assets.MVC.Controllers.JSONSchemas;
using Assets.Services.Interfaces;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Controllers.Tabs
{
	internal class DogBreedsTabController : MonoBehaviour, IController
	{
		[SerializeField]
		GameSettings GameSettings;

		[Inject]
		IRequestService RequestService;

		public async Task<string> GetRandomFact(CancellationTokenSource tokenSource)
		{
			var url = GameSettings.DogAPI.Base + GameSettings.DogAPI.RandomFact;
			var request = await RequestService.SendRequest(url, tokenSource.Token);

			var res = JsonSerializer.Deserialize<DogFactScheme>(request);

			if (res == null)
			{
				tokenSource.Cancel();
				return null;
			}

			var fact = res.data.FirstOrDefault();
			if (fact == null)
			{
				tokenSource.Cancel();
				return null;
			}

			return fact.attributes.body;
		}

		public async Task<DogBreedsSchema> GetBreeds(CancellationTokenSource tokenSource)
		{
			var url = GameSettings.DogAPI.Base + GameSettings.DogAPI.DogBreeds;
			var request = await RequestService.SendRequest(url, tokenSource.Token);

			var res = JsonSerializer.Deserialize<DogBreedsSchema>(request);

			if (res == null)
			{
				tokenSource.Cancel();
				return null;
			}

			return res;
		}

		public async Task<DogBreedSchema> GetBreedById(string id, CancellationTokenSource tokenSource)
		{
			var url = GameSettings.DogAPI.Base + GameSettings.DogAPI.DogBreedId;
			var request = await RequestService.SendRequest(url.Replace("{id}", id), tokenSource.Token);

			var res = JsonSerializer.Deserialize<DogBreedSchema>(request);

			if (res == null)
			{
				tokenSource.Cancel();
				return null;
			}

			return res;
		}
	}
}
