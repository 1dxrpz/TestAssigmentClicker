using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Models
{
	[Serializable]
	public class ForecastModel
	{
		public string IconUri { get; private set; }
		public Sprite Icon { get; private set; }
		public string Text { get; private set; }

		public void UpdateForecastIcon(string iconUri, Sprite icon)
		{
			Icon = icon;
			IconUri = iconUri;
		}

		public void UpdateForecastText(string text)
		{
			Text = text;
		}
	}
}