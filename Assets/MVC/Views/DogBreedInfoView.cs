using TMPro;
using UnityEngine;

namespace Assets.MVC.Views
{
	internal class DogBreedInfoView : MonoBehaviour
	{
		[SerializeField]
		TMP_Text Title;

		[SerializeField]
		TMP_Text Description;

		public void SetPopupVisible(bool isVisible)
		{

		}

		public void SetData(string title, string description)
		{
			Title.text = title;
			Description.text = description;
		}
	}
}
