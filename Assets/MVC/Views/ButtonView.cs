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
		IPointerUpHandler,
		IPointerClickHandler
	{
		private RawImage rawImage;
		private AudioSource audioSource;
		private bool isPressed;

		[SerializeField] private Texture Texture;
		[SerializeField] private Rect PressedOffset;
		[SerializeField] private Rect ReleasedOffset;

		[SerializeField] private AudioClip PressedAudio;
		[SerializeField] private AudioClip ReleasedAudio;

		public event Action<bool> OnButtonStateChanged;
		public event Action OnButtonClicked;

		private void Awake()
		{
			rawImage = GetComponent<RawImage>();
			audioSource = GetComponent<AudioSource>();

			if (Texture != null)
				rawImage.texture = Texture;
		}

		public void OnPointerClick(PointerEventData eventData) => OnButtonClicked?.Invoke();

		public void OnPointerDown(PointerEventData eventData) => OnButtonStateChanged?.Invoke(true);

		public void OnPointerUp(PointerEventData eventData) => OnButtonStateChanged?.Invoke(false);

		public void SetPressed(bool pressed)
		{
			if (isPressed == pressed)
				return;

			isPressed = pressed;

			rawImage.uvRect = pressed
				? PressedOffset
				: ReleasedOffset;

			AudioClip clip = pressed
				? PressedAudio
				: ReleasedAudio;

			if (clip != null)
				audioSource.PlayOneShot(clip);
		}
	}
}