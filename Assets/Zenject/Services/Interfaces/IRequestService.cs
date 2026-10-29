using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Services.Interfaces
{
	public interface IRequestService
	{
		UniTask<string> SendRequest(string uri, CancellationToken cancellationToken);
		UniTask<Sprite> DownloadImage(string uri, CancellationToken cancellationToken);
	}
}
