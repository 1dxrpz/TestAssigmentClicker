using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Views
{
	internal class TabPageView : MonoBehaviour
	{
		public void SetPageVisible(bool isVisible)
		{
			gameObject.SetActive(isVisible);
		}
	}
}
