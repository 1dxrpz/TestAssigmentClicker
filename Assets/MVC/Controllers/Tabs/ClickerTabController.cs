using Assets.Controllers.Interfaces;
using Assets.Models;
using Assets.Views;
using System.Collections;
using UnityEngine;
using Zenject;
using static UIParticle;

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

		[SerializeField] RectTransform ParticlesSpawnArea;
		[SerializeField] private int amount = 20;
		[SerializeField] private float lifetime = 1.5f;
		[SerializeField] private float speed = 100f;
		[SerializeField, Range(0f, 1f)] private float shrinkStart = 0.7f;

		UIParticleFactory ParticleFactory;

		[Inject]
		public void Construct(UIParticleFactory factory)
		{
			ParticleFactory = factory;
		}

		public void SpawnParticles()
		{
			if (!ParticlesSpawnArea.gameObject.activeInHierarchy)
				return;

			for (int i = 0; i < amount; i++)
			{
				UIParticle particle = ParticleFactory.Create();

				RectTransform rect = particle.GetComponent<RectTransform>();
				rect.SetParent(ParticlesSpawnArea, false);
				rect.anchoredPosition = Vector2.zero;
				rect.localScale = Vector3.one;

				Vector2 direction = Random.insideUnitCircle.normalized;
				particle.Initialize(direction, speed, lifetime, shrinkStart);
			}
		}

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
			Model.SetEnergy(Model.Energy - GameSettings.ClickEnergyCost);

			SpawnParticles();
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
