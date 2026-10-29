using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.MVC.Views
{
	internal class DogBreedView : MonoBehaviour,
		IPointerClickHandler
	{
		[SerializeField]
		TMP_Text NameText;

		[SerializeField]
		TMP_Text IndexText;

		[SerializeField]
		GameObject LoadingCircle;

		public event Action OnDogBreedClicked;

		public void OnPointerClick(PointerEventData eventData)
		{
			OnDogBreedClicked?.Invoke();
		}

		public void SetData(int index, string name)
		{
			NameText.text = name;
			IndexText.text = $"{index}.";
		}

		public void SetLoadingCircleVisible(bool isVisible)
		{
			LoadingCircle.SetActive(isVisible);
		}

		public class DogBreedViewFactory : PlaceholderFactory<DogBreedView> { }
	}
}
