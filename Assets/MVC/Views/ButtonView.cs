using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Views
{

	[RequireComponent(typeof(RawImage))]
	[RequireComponent(typeof(AudioSource))]
	public class ButtonView : MonoBehaviour,
		IPointerDownHandler,
		IPointerUpHandler
	{
		private RawImage RawImageComponent;
		private AudioSource AudioSource;

		[SerializeField] Texture Texture;
		[SerializeField] Rect PressedOffset;
		[SerializeField] Rect ReleasedOffset;

		[SerializeField] AudioClip PressedAudio;
		[SerializeField] AudioClip ReleasedAudio;

		public event Action<bool> OnButtonStateChanged;

		public void OnPointerDown(PointerEventData eventData)
		{
			OnButtonStateChanged?.Invoke(true);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			OnButtonStateChanged?.Invoke(false);
		}

		protected void Start()
		{
			RawImageComponent = GetComponent<RawImage>();
			AudioSource = GetComponent<AudioSource>();
		}

		protected void OnValidate()
		{
			if (RawImageComponent == null)
				RawImageComponent = GetComponent<RawImage>();

			if (Texture != null)
				RawImageComponent.texture = Texture;
		}

		public void SetPressed(bool isPressed)
		{
			RawImageComponent.uvRect = isPressed ? PressedOffset : ReleasedOffset;
			AudioSource.PlayOneShot(isPressed ? PressedAudio : ReleasedAudio);
		}
	}
}