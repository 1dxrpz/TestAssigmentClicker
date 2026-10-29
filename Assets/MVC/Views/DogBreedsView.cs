using TMPro;
using UnityEngine;

namespace Assets.Views
{
	internal class DogBreedsView : TabPageView
	{
		[SerializeField]
		CanvasGroup RandomFactLoading;

		[SerializeField]
		TMP_Text FactText;

		public void HideRandomFact()
		{
			RandomFactLoading.blocksRaycasts = false;
			RandomFactLoading.alpha = 0;
		}

		public void ShowRandomFact(string text)
		{
			FactText.text = text;
			RandomFactLoading.blocksRaycasts = true;
			RandomFactLoading.alpha = 1;
		}
	}
}