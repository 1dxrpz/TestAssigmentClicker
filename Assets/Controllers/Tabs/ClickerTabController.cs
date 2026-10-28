using Assets.Controllers.Interfaces;
using Assets.Models;
using Assets.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Controllers.Tabs
{
	internal class ClickerTabController : MonoBehaviour, IController
	{
		ClickerModel Model = new();

		[SerializeField]
		ClickerView ClickerView;

		[SerializeField]
		GameSettings GameSettings;

		void Awake()
		{
			Model.OnCurrencyChanged += UpdateCurrency;
			Model.OnEnergyChanged += UpdateEnergy;

			Model.SetMaxEnergy(GameSettings.MaxEnergy);
			Model.SetEnergy(GameSettings.StartEnergy);
		}

		void Start()
		{
			StartCoroutine(AutoGainEnergy());
		}

		IEnumerator AutoGainEnergy()
		{
			yield return new WaitForSeconds(GameSettings.IterationTimeInSeconds);
			Model.SetEnergy(Model.Energy + GameSettings.EnergyPerIteration);
		}

		private void UpdateEnergy(int value)
		{
			ClickerView.UpdateEnergy(value);
		}

		private void UpdateCurrency(int value)
		{
			ClickerView.UpdateCurrency(value);
		}
	}
}
