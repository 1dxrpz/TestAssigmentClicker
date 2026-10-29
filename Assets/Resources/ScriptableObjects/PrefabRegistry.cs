using Assets.MVC.Views;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Resources.ScriptableObjects
{
	[CreateAssetMenu(fileName = "PrefabRegistry", menuName = "Project Settings/Prefab Registry")]
	internal class PrefabRegistry : ScriptableObject
	{
		[SerializeField]
		UIParticle particlePrefab;
		public UIParticle ParticlePrefab => particlePrefab;

		[SerializeField]
		DogBreedView dogBreedViewPrefab;
		public DogBreedView DogBreedViewPrefab => dogBreedViewPrefab;
	}
}
