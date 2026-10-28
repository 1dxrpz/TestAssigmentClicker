using Assets.Services.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Services
{
	internal class RequestService : IRequestService, IService
	{
		public async UniTask<string> SendRequest(string uri, CancellationToken cancellationToken)
		{
			try
			{
				var request = UnityWebRequest
					.Get(uri);

				await request.SendWebRequest()
					.WithCancellation(cancellationToken);

				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError(request.error);
					return null;
				}

				return request.downloadHandler.text;
			}
			catch (OperationCanceledException)
			{
				Debug.Log("operation cancelled");
				return null;
			}
		}

		public async UniTask<Sprite> DownloadImage(string uri, CancellationToken cancellationToken)
		{
			try
			{
				using var request = UnityWebRequestTexture.GetTexture(uri);

				await request.SendWebRequest()
					.WithCancellation(cancellationToken);

				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError(request.error);
					return null;
				}

				var texture = DownloadHandlerTexture.GetContent(request);

				return Sprite.Create(
					texture,
					new Rect(0, 0, texture.width, texture.height),
					new Vector2(0.5f, 0.5f)
				);
			}
			catch (OperationCanceledException)
			{
				Debug.Log("Image download cancelled");
				return null;
			}
		}
	}
}
