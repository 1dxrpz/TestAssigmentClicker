using Assets.Views;
using System;
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

		[SerializeField]
		ButtonView OkButton;

		public event Action OnOkClicked;

		private void Awake()
		{
			OkButton.OnButtonClicked += () =>
			{
				OnOkClicked?.Invoke();
			};
		}

		public void HidePopup()
		{
			gameObject.SetActive(false);
		}

		public void ShowPopup(string title, string description)
		{
			gameObject.SetActive(true);
			Title.text = title;
			Description.text = description;
		}
	}
}
