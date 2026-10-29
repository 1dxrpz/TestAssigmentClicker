using Assets.Controllers.Interfaces;
using Assets.MVC.Controllers.JSONSchemas;
using Assets.MVC.Models;
using Assets.MVC.Views;
using Assets.Services.Interfaces;
using Assets.Views;
using System;
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

		[Inject]
		TabController TabController;
		private CancellationTokenSource CancellationToken;

		[SerializeField]
		DogBreedsView DogBreedsView;

		[SerializeField]
		DogBreedInfoView Popup;

		private readonly DogBreedsModel Model = new();

		private CancellationTokenSource _breedFetchCts;

		void Awake()
		{
			TabController.OnTabChanged += OnTabChanged;
			Popup.OnOkClicked += Popup.HidePopup;
		}

		private void CreateDogBreedViews()
		{
			Model.DogBreeds.Select((v, i) =>
			{
				var breed = DogBreedsView.CreateDogBreedView(i + 1, v.attributes.name);
				breed.SetLoadingCircleVisible(false);
				breed.OnDogBreedClicked += () =>
				{
					OnDogBreedClicked(breed, v);
				};

				return v;
			}).ToList();
		}

		private void OnDogBreedClicked(DogBreedView breed, DogData data)
		{
			_ = FetchDogBreed(breed, data);
		}

		private async Task FetchDogBreed(DogBreedView breed, DogData data)
		{
			_breedFetchCts?.Cancel();
			_breedFetchCts?.Dispose();

			_breedFetchCts = new CancellationTokenSource();
			var token = _breedFetchCts.Token;

			breed.SetLoadingCircleVisible(true);

			DogBreedSchema fetchedData = null;

			try
			{
				token.ThrowIfCancellationRequested();
				// ?? тут не обязательно подтягивать по id, потому, что /breeds уже содержит нужную информацию
				fetchedData = await GetBreedById(data.id, _breedFetchCts);
			}
			catch (OperationCanceledException)
			{
				breed.SetLoadingCircleVisible(false);
				Debug.Log("Dog breed fetch canceled");
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			finally
			{
				if (_breedFetchCts != null &&
					!token.IsCancellationRequested &&
					fetchedData != null)
				{
					ShowInfoPopup(fetchedData);
				}
				breed.SetLoadingCircleVisible(false);
			}
		}

		private void ShowInfoPopup(DogBreedSchema fetchedData)
		{
			var attr = fetchedData.data.attributes;
			Popup.ShowPopup(attr.name, attr.description);
		}

		private void OnTabChanged(TabPageView view)
		{
			if (view.GetType() == typeof(DogBreedsView))
			{
				_ = StartPage();
			}
			else
			{
				_breedFetchCts?.Cancel();

				DogBreedsView.ClearDogBreedViews();
				CancellationToken?.Cancel();
			}
		}

		private async Awaitable StartPage()
		{
			CancellationToken = new();
			DogBreedsView.SetLoadingCircleVisible(true);

			try
			{
				var breeds = await GetBreeds(CancellationToken);
				Model.SetDogBreeds(breeds.data);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			finally
			{
				DogBreedsView.SetLoadingCircleVisible(false);
				CreateDogBreedViews();
			}
		}

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
