using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Models
{
	[Serializable]
	public class Period
	{
		public int number { get; set; }
		public string name { get; set; }
		public DateTime startTime { get; set; }
		public DateTime endTime { get; set; }
		public bool isDaytime { get; set; }
		public int temperature { get; set; }
		public string temperatureUnit { get; set; }
		public string icon { get; set; }
	}

	[Serializable]
	public class Properties
	{
		public List<Period> periods { get; set; }
	}

	[Serializable]
	public class ForecastData
	{
		public Properties properties { get; set; }
	}
}
