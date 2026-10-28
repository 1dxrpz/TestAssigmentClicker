using System;

namespace Assets.Models
{
	internal class ClickerModel
	{
		public int MaxEnergy { get; private set; }
		public int Energy { get; private set; }
		public int Currency { get; private set; }

		public event Action<int> OnEnergyChanged;
		public event Action<int> OnCurrencyChanged;

		public void SetMaxEnergy(int value)
		{
			MaxEnergy = Math.Clamp(value, 0, value);
		}

		public void SetCurrency(int value)
		{
			if (value == Currency) return;

			Currency = value;
			OnCurrencyChanged?.Invoke(Currency);
		}

		public void SetEnergy(int value)
		{
			value = Math.Clamp(value, 0, MaxEnergy);
			if (Energy <= 0 || value == Energy) return;
			Energy = value;
			OnEnergyChanged?.Invoke(Energy);
		}
	}
}
