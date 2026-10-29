using Assets.MVC.Views;
using TMPro;
using UnityEngine;
using Zenject;
using static Assets.MVC.Views.DogBreedView;

namespace Assets.Views
{
	internal class DogBreedsView : TabPageView
	{
		[SerializeField]
		CanvasGroup RandomFactLoading;

		[SerializeField]
		TMP_Text FactText;

		[SerializeField] RectTransform ContentArea;

		[Inject]
		DogBreedViewFactory DogBreedViewFactory;

		public void CreateDogBreedView(int index, string name)
		{
			if (!ContentArea.gameObject.activeInHierarchy)
				return;

			DogBreedView BreedView = DogBreedViewFactory.Create();

			RectTransform rect = BreedView.GetComponent<RectTransform>();
			rect.SetParent(ContentArea, false);
			rect.anchoredPosition = Vector2.zero;
			rect.localScale = Vector3.one;

			BreedView.SetData(index, name);
		}
	}
}