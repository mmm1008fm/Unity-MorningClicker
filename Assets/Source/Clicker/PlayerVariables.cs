using UnityEngine;
using UnityEngine.Events;

public static class PlayerVariables
{
	public static int Warriors = 0;
	public static int PerClick = 1;
	public static float DefensePercent = 0f;
	public static float MusicVolume = 1f;
	public static float GameVolume = 1f;
	public static int WindmillPower = 0;
	
	public static event UnityAction OnScoreChanged;

	private static int _score;
	
	public static int Score
	{
		get => _score;
		set
		{
			_score = value;
			OnScoreChanged?.Invoke();
			//Debug.Log("Изменение очков и вызов");
		}
	}

}
