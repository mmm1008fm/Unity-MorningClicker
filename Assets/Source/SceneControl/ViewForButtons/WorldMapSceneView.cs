using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapSceneView : BaseInitializable
{
	[SerializeField] private Button _exit;
	[SerializeField] private List<Button> _enemiesZone;

	public override void Initialize()
	{
		_exit.onClick.AddListener(Exit);
		foreach (var buyButton in _enemiesZone)
		{
			var buttonName = buyButton.name;
			buyButton.onClick.AddListener(() => GoToEnemyWar(buttonName));
		}
	}

	private void Exit()
	{
		SceneSwitcher.Switch("Main");
	}
    
	private void GoToEnemyWar(string buttonName)
	{
		throw new Exception($"War have not yet been implemented (Button: {buttonName})");
	}
}