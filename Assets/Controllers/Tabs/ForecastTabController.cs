using Assets.Controllers.Interfaces;
using Assets.Models;
using Assets.Services.Interfaces;
using Assets.Views;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

#nullable enable

namespace Assets.Controllers.Tabs
{
	public class ForecastTabController : MonoBehaviour, IController
	{
		[Inject]
		IRequestService RequestService;

		[SerializeField]
		APISettings APISettings;

		[SerializeField]
		ForecastView? ForecastView = null;

		private readonly ForecastModel Model = new();
		private readonly CancellationTokenSource CancellationToken = new();

		void Awake()
		{
			Model.OnForecastFetched += OnForecastFetched;
		}

		void Start()
		{
			_ = RepeatSeconds(2.0f, CancellationToken.Token);

			ForecastView?.SetLoadingCircleVisible(true);
		}
		void OnDestroy()
		{
			CancellationToken.Cancel();
			CancellationToken.Dispose();
		}

		async Task RequestForecast()
		{
			var request = await RequestService
				.SendRequest(APISettings.ForecastAPI, CancellationToken.Token);

			CancellationToken.Token.ThrowIfCancellationRequested();

			var res = JsonSerializer.Deserialize<ForecastData>(request);

			if (res == null)
			{
				return;
			}

			var period = res.properties.periods.First();

			Model.UpdateForecastText($"{period.name}: {period.temperature}{period.temperatureUnit}");

			if (!string.Equals(Model.IconUri, period.icon, StringComparison.OrdinalIgnoreCase))
			{
				var sprite = await RequestService
					.DownloadImage(period.icon, CancellationToken.Token);

				Model.UpdateForecastIcon(period.icon, sprite);
			}

			Model.FetchForecastData();
		}

		private void OnForecastFetched()
		{
			ForecastView.SetLoadingCircleVisible(false);
			ForecastView.UpdateData(Model);
		}

		private async Awaitable RepeatSeconds(float seconds, CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				await RequestForecast();
				await Awaitable.WaitForSecondsAsync(seconds);
			}
		}

	}
}
