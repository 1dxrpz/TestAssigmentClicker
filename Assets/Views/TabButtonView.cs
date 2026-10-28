using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Views
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(AudioSource))]
	public class TabButtonView : MonoBehaviour,
		IPointerClickHandler
	{
		public event Action OnTabClicked;

		[SerializeField]
		GameObject SelectionBrackets;

		[SerializeField]
		List<AudioClip> AudioClips;

		[SerializeField]
		Sprite SelectedTabTexture;

		[SerializeField]
		Sprite DefaultTabTexture;

		private AudioSource AudioSource;
		private Image ImageComponent;

		void Awake()
		{
			AudioSource = GetComponent<AudioSource>();
			ImageComponent = GetComponent<Image>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnTabClicked?.Invoke();

			var count = AudioClips.Count;
			if (count != 0)
			{
				var index = UnityEngine.Random.Range(0, count - 1);
				var clip = AudioClips[index];
				AudioSource.PlayOneShot(clip);
			}
		}

		public void SetSelected(bool isSelected)
		{
			ImageComponent.sprite = isSelected ? SelectedTabTexture : DefaultTabTexture;
			SelectionBrackets.SetActive(isSelected);
		}
	}
}