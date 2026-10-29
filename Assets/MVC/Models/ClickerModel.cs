using System;

namespace Assets.Models
{
	internal class ClickerModel
	{
		private readonly int ClickEnergyCost;

		public int MaxEnergy { get; private set; }
		public int Energy { get; private set; }
		public int Currency { get; private set; }

		public bool IsClickInProgress { get; private set; }

		public bool CanClick => Energy >= ClickEnergyCost;

		public event Action<int> OnEnergyChanged;
		public event Action<int> OnCurrencyChanged;

		public ClickerModel(int clickEnergyCost)
		{
			ClickEnergyCost = Math.Max(0, clickEnergyCost);
		}

		public bool TryStartClick()
		{
			if (IsClickInProgress || !CanClick)
				return false;

			IsClickInProgress = true;

			return true;
		}

		public bool CompleteClick(int currencyReward)
		{
			if (!IsClickInProgress)
				return false;

			IsClickInProgress = false;

			if (!CanClick)
				return false;

			SetEnergy(Energy - ClickEnergyCost);
			SetCurrency(Currency + Math.Max(0, currencyReward));

			return true;
		}

		public void CancelClick()
		{
			IsClickInProgress = false;
		}

		public void SetMaxEnergy(int value)
		{
			MaxEnergy = Math.Max(0, value);
			SetEnergy(Energy);
		}

		public void SetEnergy(int value)
		{
			value = Math.Clamp(value, 0, MaxEnergy);

			if (value == Energy)
				return;

			Energy = value;
			OnEnergyChanged?.Invoke(Energy);
		}

		public void SetCurrency(int value)
		{
			value = Math.Max(0, value);

			if (value == Currency)
				return;

			Currency = value;
			OnCurrencyChanged?.Invoke(Currency);
		}
	}
}