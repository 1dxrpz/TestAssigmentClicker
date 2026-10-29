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
		private ClickerModel Model;

		[SerializeField] private ClickerView ClickerView;
		[SerializeField] private ButtonView ButtonView;
		[SerializeField] private GameSettings GameSettings;

		[SerializeField] private RectTransform ParticlesSpawnArea;
		[SerializeField] private int amount = 20;
		[SerializeField] private float lifetime = 1.5f;
		[SerializeField] private float speed = 100f;
		[SerializeField, Range(0f, 1f)]
		private float shrinkStart = 0.7f;

		[Inject]
		private UIParticleFactory ParticleFactory;

		private const float ReleaseButtonAnimation = 0.2f;

		private void Awake()
		{
			Model = new ClickerModel(
				GameSettings.ClickEnergyCost
			);

			Model.OnCurrencyChanged += UpdateCurrency;
			Model.OnEnergyChanged += UpdateEnergy;

			ButtonView.OnButtonStateChanged += ChangeButtonState;
		}

		private void Start()
		{
			Model.SetMaxEnergy(GameSettings.MaxEnergy);
			Model.SetEnergy(GameSettings.StartEnergy);
			Model.SetCurrency(0);

			UpdateCurrency(Model.Currency);
			UpdateEnergy(Model.Energy);

			StartCoroutine(AutoGainEnergy());
			StartCoroutine(AutoClick());
		}

		private void OnDestroy()
		{
			if (Model != null)
			{
				Model.OnCurrencyChanged -= UpdateCurrency;
				Model.OnEnergyChanged -= UpdateEnergy;
			}

			if (ButtonView != null)
				ButtonView.OnButtonStateChanged -= ChangeButtonState;

			StopAllCoroutines();

			Model?.CancelClick();
		}

		private void ChangeButtonState(bool isPressed)
		{
			if (isPressed)
			{
				if (!Model.TryStartClick())
					return;

				ButtonView.SetPressed(true);
				return;
			}

			if (!Model.IsClickInProgress)
				return;

			ButtonView.SetPressed(false);

			if (Model.CompleteClick(GameSettings.CurrencyPerClick))
				SpawnParticles();
		}

		private void UpdateEnergy(int value)
		{
			ClickerView.UpdateEnergy(value);
		}

		private void UpdateCurrency(int value)
		{
			ClickerView.UpdateCurrency(value);
		}

		private IEnumerator AutoGainEnergy()
		{
			while (true)
			{
				yield return new WaitForSeconds(
					GameSettings.EnergyAutogainDuration
				);

				Model.SetEnergy(
					Model.Energy + GameSettings.EnergyAutogainAmount
				);
			}
		}

		private IEnumerator AutoClick()
		{
			while (true)
			{
				yield return new WaitForSeconds(
					GameSettings.AutoclickDuration
				);

				// Не прерываем уже начатый клик.
				if (Model.IsClickInProgress || !Model.CanClick)
					continue;

				ChangeButtonState(true);

				if (Model.IsClickInProgress)
					StartCoroutine(ReleaseButton());
			}
		}

		private IEnumerator ReleaseButton()
		{
			yield return new WaitForSeconds(ReleaseButtonAnimation);
			ChangeButtonState(false);
		}

		public void SpawnParticles()
		{
			if (!ParticlesSpawnArea.gameObject.activeInHierarchy)
				return;

			for (int i = 0; i < amount; i++)
			{
				UIParticle particle = ParticleFactory.Create();

				RectTransform rect =
					particle.GetComponent<RectTransform>();

				rect.SetParent(ParticlesSpawnArea, false);
				rect.anchoredPosition = Vector2.zero;
				rect.localScale = Vector3.one;

				Vector2 direction =
					Random.insideUnitCircle.normalized;

				particle.Spawn(
					direction,
					speed,
					lifetime,
					shrinkStart
				);
			}
		}
	}
}