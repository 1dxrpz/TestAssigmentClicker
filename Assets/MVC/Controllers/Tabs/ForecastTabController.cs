using Assets.Controllers.Interfaces;
using Assets.Models;
using Assets.MVC.Controllers.JSONSchemas;
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
		GameSettings GameSettings;

		[SerializeField]
		ForecastView ForecastView;

		[Inject]
		TabController TabController;

		private readonly ForecastModel Model = new();
		private CancellationTokenSource? CancellationToken;

		void Awake()
		{
			TabController.OnTabChanged += OnTabChanged;
		}

		private void OnTabChanged(TabPageView view)
		{
			if (view.GetType() == typeof(ForecastView))
			{
				StartPage();
			}
			else
			{
				CancellationToken?.Cancel();
			}
		}

		void StartPage()
		{
			CancellationToken = new();
			_ = RepeatSeconds(2.0f, CancellationToken.Token);
			ForecastView.SetLoadingCircleVisible(true);
		}

		void OnDestroy()
		{
			CancellationToken?.Cancel();
			CancellationToken?.Dispose();
		}

		async Task RequestForecast(CancellationToken token)
		{
			try
			{
				var request = await RequestService.SendRequest(
					GameSettings.ForecastAPI,
					token
				);

				token.ThrowIfCancellationRequested();

				var res = await Task.Run(
					() => JsonSerializer.Deserialize<ForecastSchema>(request),
					token
				);

				if (res == null)
					return;

				var period = res.properties.periods.First();

				Model.UpdateForecastText($"{period.name}: {period.temperature}{period.temperatureUnit}");

				if (!string.Equals(Model.IconUri, period.icon, StringComparison.OrdinalIgnoreCase))
				{
					var sprite = await RequestService
						.DownloadImage(period.icon, token);

					Model.UpdateForecastIcon(period.icon, sprite);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			finally
			{
				ForecastView.SetLoadingCircleVisible(false);
				ForecastView.UpdateData(Model);
			}
		}

		private async Awaitable RepeatSeconds(float seconds, CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				await RequestForecast(token);
				await Awaitable.WaitForSecondsAsync(seconds);
			}
		}

	}
}
