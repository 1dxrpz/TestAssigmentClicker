using Assets.Controllers.Interfaces;
using Assets.Models;
using Assets.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controllers
{
	internal class TabController : MonoBehaviour, IController
	{
		TabModel TabModel = new();

		[SerializeField]
		Dictionary<TabButtonView, TabPageView> Tabs = new();

		private void Awake()
		{
			TabModel.OnTabSelected += OnTabSelected;

			Tabs.Select((v, i) =>
			{
				v.Key.OnTabClicked += () => { TabModel.SelectTab(i); };
				return v;
			}).ToList();
		}

		void Start()
		{
			TabModel.SelectTab(0);
		}

		private void OnTabSelected(int index)
		{
			Tabs.Select((v, i) =>
			{
				var value = index == i;
				v.Key.SetSelected(value);
				v.Value.SetPageVisible(value);
				return v;
			}).ToList();
		}
	}
}
