using System;
using System.Collections.Generic;

namespace Assets.MVC.Controllers.JSONSchemas
{
	[Serializable]
	public class BreedAttributes
	{
		public string name { get; set; }
		public string description { get; set; }
	}

	[Serializable]
	public class DogData
	{
		public BreedAttributes attributes { get; set; }
	}
	
	[Serializable]
	public class DogBreedSchema
	{
		public DogData data { get; set; }
	}

	[Serializable]
	public class DogBreedsSchema
	{
		public List<DogData> data { get; set; }
	}
}
