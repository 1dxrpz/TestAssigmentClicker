using Assets.MVC.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using static Assets.MVC.Views.DogBreedView;

namespace Assets.Views
{
	internal class DogBreedsView : TabPageView
	{
		[SerializeField] RectTransform ContentArea;

		[Inject]
		DogBreedViewFactory DogBreedViewFactory;

		[SerializeField]
		GameObject LoadingCircle;

		[SerializeField]
		GameObject DogBreedsWrapper;

		List<DogBreedView> DogBreedViews = new();

		public void ClearDogBreedViews()
		{
			foreach (var item in DogBreedViews)
			{
				Destroy(item.gameObject);
			}
			DogBreedViews.Clear();
		}

		public DogBreedView CreateDogBreedView(int index, string name)
		{
			DogBreedView breedView = DogBreedViewFactory.Create();

			RectTransform rect = breedView.GetComponent<RectTransform>();
			rect.SetParent(ContentArea, false);
			rect.anchoredPosition = Vector2.zero;
			rect.localScale = Vector3.one;

			breedView.SetData(index, name);
			DogBreedViews.Add(breedView);

			return breedView;
		}

		public void SetLoadingCircleVisible(bool isVisible)
		{
			LoadingCircle.SetActive(isVisible);
			DogBreedsWrapper.SetActive(!isVisible);
		}
	}
}