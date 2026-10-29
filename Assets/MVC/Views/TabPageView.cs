using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Views
{
	[RequireComponent(typeof(CanvasGroup))]
	internal abstract class TabPageView : MonoBehaviour
	{
		CanvasGroup CanvasGroup;

		private void Awake()
		{
			CanvasGroup = GetComponent<CanvasGroup>();
		}

		public void SetPageVisible(bool isVisible)
		{
			CanvasGroup.interactable = isVisible;
			CanvasGroup.alpha = isVisible ? 1.0f : 0;
			CanvasGroup.blocksRaycasts = isVisible;
		}
	}
}
