using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.MVC.Views
{
	internal class DogBreedView : MonoBehaviour
	{
		[SerializeField]
		TMP_Text NameText;

		[SerializeField]
		TMP_Text IndexText;

		public void SetData(int index, string name)
		{
			NameText.text = name;
			IndexText.text = $"{index}.";
		}

		public class DogBreedViewFactory : PlaceholderFactory<DogBreedView> { }
	}
}
