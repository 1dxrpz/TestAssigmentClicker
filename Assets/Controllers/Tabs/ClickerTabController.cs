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
		ButtonView ButtonView;

		[SerializeField]
		GameSettings GameSettings;

		const float ReleaseButtonAnimation = 0.2f;

		void Awake()
		{
			Model.OnCurrencyChanged += UpdateCurrency;
			Model.OnEnergyChanged += UpdateEnergy;

			ButtonView.OnButtonStateChanged += ChangeButtonState;

			Model.OnButtonClicked += OnButtonClicked;
		}

		private void OnButtonClicked()
		{
			Model.SetCurrency(Model.Currency + GameSettings.CurrencyPerClick);
			Model.SetEnergy(Model.MaxEnergy - GameSettings.ClickEnergyCost);
		}

		private void ChangeButtonState(bool isPressed)
		{
			if (Model.Energy >= GameSettings.ClickEnergyCost)
			{
				Model.ButtonState = isPressed;
				ButtonView.SetPressed(isPressed);
				if (!isPressed)
				{
					Model.ClickButton();
				}
			}
		}

		void Destroy()
		{
			Model.OnCurrencyChanged -= UpdateCurrency;
			Model.OnEnergyChanged -= UpdateEnergy;
		}

		void Start()
		{
			StartCoroutine(AutoGainEnergy());
			StartCoroutine(AutoClick());

			Model.SetMaxEnergy(GameSettings.MaxEnergy);
			Model.SetEnergy(GameSettings.StartEnergy);

			Model.SetCurrency(0);
		}

		IEnumerator AutoClick()
		{
			while (true)
			{
				yield return new WaitForSeconds(GameSettings.AutoclickDuration);
				ChangeButtonState(true);
				if (Model.ButtonState)
				{
					StartCoroutine(ReleaseButton());
				}
			}
		}

		IEnumerator ReleaseButton()
		{
			yield return new WaitForSeconds(ReleaseButtonAnimation);
			ChangeButtonState(false);
		}

		IEnumerator AutoGainEnergy()
		{
			while (true)
			{
				yield return new WaitForSeconds(GameSettings.EnergyAutogainDuration);
				Model.SetEnergy(Model.Energy + GameSettings.EnergyAutogainAmount);
			}
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
