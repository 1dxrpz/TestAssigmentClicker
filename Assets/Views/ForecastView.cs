using Assets.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views
{
	internal class ForecastView : MonoBehaviour
	{
		[SerializeField]
		GameObject ForecastWrapper;

		[SerializeField]
		GameObject LoadingCircle;

		[SerializeField]
		Image ForecastIcon;

		[SerializeField]
		TMP_Text ForecastText;


		public void UpdateData(ForecastModel model)
		{
			ForecastText.text = model.Text;
			ForecastIcon.sprite = model.Icon;
		}

		public void SetLoadingCircleVisible(bool isVisible)
		{
			LoadingCircle.SetActive(isVisible);
			ForecastWrapper.SetActive(!isVisible);
		}
	}
}
