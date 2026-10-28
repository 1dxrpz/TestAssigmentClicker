using System;
using UnityEngine;

namespace Assets.Models
{
	public class TabModel
	{
		public int SelectedTab { get; private set; } = -1;

		public event Action<int> OnTabSelected;

		public void SelectTab(int index)
		{
			if (SelectedTab == index)
				return;

			SelectedTab = index;
			OnTabSelected?.Invoke(index);
		}
	}
}