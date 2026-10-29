using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.MVC.Controllers.JSONSchemas
{
	[Serializable]
	public class FactAttribute
	{
		public string body { get; set; }
	}

	[Serializable]
	public class FactData
	{
		public FactAttribute attributes { get; set; }
	}

	internal class DogFactScheme
	{
		public List<FactData> data { get; set; }
	}
}
