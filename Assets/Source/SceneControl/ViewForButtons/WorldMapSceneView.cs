using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapSceneView : BaseInitializable
{
	[SerializeField] private Button _exit;
	[SerializeField] private List<Button> _enemiesZone; // TODO: Добавить реализацию для всех кнопок и вообще переделать всё в зоны

	public override void Initialize()
	{
		_exit.onClick.AddListener(Exit);
	}

	private void Exit() => SceneSwitcher.Switch("Main");
}