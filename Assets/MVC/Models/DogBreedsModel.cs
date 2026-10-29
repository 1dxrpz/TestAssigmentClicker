using Assets.MVC.Controllers.JSONSchemas;
using System;
using System.Collections.Generic;

namespace Assets.MVC.Models
{
	internal class DogBreedsModel
	{
		public event Action OnDogBreedsFetched;

		List<DogData> dogBreeds = new();

		public List<DogData> DogBreeds => dogBreeds;

		public void SetDogBreeds(List<DogData> breeds)
		{
			dogBreeds = breeds;
			OnDogBreedsFetched?.Invoke();
		}
	}
}
