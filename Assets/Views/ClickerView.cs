using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Views
{
	internal class ClickerView : MonoBehaviour
	{
		[SerializeField]
		TMP_Text CurrencyText;

		[SerializeField]
		TMP_Text EnergyText;

		public void UpdateCurrency(int value)
		{
			CurrencyText.text = $"{value}";
		}

		public void UpdateEnergy(int value)
		{
			EnergyText.text = $"{value}";
		}
	}
}
