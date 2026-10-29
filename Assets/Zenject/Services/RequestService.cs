using Assets.Services.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Services
{
	internal class RequestService : IRequestService, IService
	{
		public async UniTask<string> SendRequest(
			string uri,
			CancellationToken cancellationToken)
		{
			try
			{
				using var request = UnityWebRequest.Get(uri);

				await request.SendWebRequest()
					.WithCancellation(cancellationToken);

				cancellationToken.ThrowIfCancellationRequested();

				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError($"Request failed: {request.error}");
					return null;
				}

				return request.downloadHandler.text;
			}
			catch (OperationCanceledException)
			{
				Debug.Log("Operation was cancelled");
				return null;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				return null;
			}
		}

		public async UniTask<Sprite> DownloadImage(
			string uri,
			CancellationToken cancellationToken)
		{
			try
			{
				using var request =
					UnityWebRequestTexture.GetTexture(uri, nonReadable: true);

				await request.SendWebRequest()
					.WithCancellation(cancellationToken);

				cancellationToken.ThrowIfCancellationRequested();

				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError($"Image download failed: {request.error}");
					return null;
				}

				var texture = DownloadHandlerTexture.GetContent(request);

				cancellationToken.ThrowIfCancellationRequested();

				return Sprite.Create(
					texture,
					new Rect(0, 0, texture.width, texture.height),
					new Vector2(0.5f, 0.5f)
				);
			}
			catch (OperationCanceledException)
			{
				Debug.Log("Operation was cancelled");
				return null;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				return null;
			}
		}
	}
}