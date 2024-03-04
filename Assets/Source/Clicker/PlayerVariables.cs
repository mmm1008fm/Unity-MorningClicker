using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerVariables
{
	public static PlayerVariables Singleton;
	public int Warriors = 0;
	public int PerClick = 1;
	public float DefensePercent = 0f;
	public float MusicVolume = 1f;
	public float GameVolume = 1f;
	public int WindmillPower = 0;
	[SerializeField] private bool _resetProgress = false;

public event UnityAction OnScoreChanged;

	private int _score;
	
	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			OnScoreChanged?.Invoke();
			SaveSystem.SaveClickerData(this);
		}
	}

	public PlayerVariables()
	{
		Singleton = this;
		if (!_resetProgress)
		{
			Warriors = SaveSystem.LoadClickerData().Warriors;
			PerClick = SaveSystem.LoadClickerData().PerClick;
			DefensePercent = SaveSystem.LoadClickerData().DefensePercent;
			MusicVolume = SaveSystem.LoadClickerData().MusicVolume;
			GameVolume = SaveSystem.LoadClickerData().GameVolume;
			WindmillPower = SaveSystem.LoadClickerData().WindmillPower;
			Score = SaveSystem.LoadClickerData().Score;
		}
		else
		{
			SaveSystem.DeleteSaveFile();
		}
	}
}
