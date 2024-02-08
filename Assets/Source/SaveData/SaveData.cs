using System.Collections;
using UnityEngine;

public class SaveData : BaseInitializable
{
	[SerializeField] private float _autoSaveInterval = 10f;
	[SerializeField] private bool _resetProgress = false;
	private static SaveData singleton;
	
	public override void Initialize()
	{
		PlayerVariables.IsResetProgress = _resetProgress;
		
		if (!singleton)
		{
			DontDestroyOnLoad(gameObject);
			singleton = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
		Debug.LogWarning(PlayerVariables.IsResetProgress);
		
		if (PlayerVariables.IsResetProgress)
		{
			Debug.Log("Data not loaded, because the progress reset is on");
			PlayerVariables.IsResetProgress = false;
			PlayerPrefs.DeleteAll(); // Не работает -> Очень даже удаляется
			Debug.Log("Data delited");
			LoadData();
		}
		else
		{
			LoadData();
		}
		
		StartCoroutine(SaveCycle(_autoSaveInterval));
	}
	
	private IEnumerator SaveCycle(float _interval)
	{
		while (Application.isPlaying)
		{
			SetData();
			yield return new WaitForSeconds(_interval);
		}
	}

	private void OnApplicationQuit()
	{
		SetData();
	}

	private void SetData()
	{
		PlayerPrefs.SetInt("Score", PlayerVariables.Score);
		PlayerPrefs.SetInt("PerClick", PlayerVariables.PerClick);
		PlayerPrefs.SetInt("Warriors", PlayerVariables.Warriors);
		PlayerPrefs.SetFloat("DefensePercent", PlayerVariables.DefensePercent);
		PlayerPrefs.SetFloat("GameVolume", PlayerVariables.GameVolume);
		PlayerPrefs.SetFloat("MusicVolume", PlayerVariables.MusicVolume);
		PlayerPrefs.SetInt("WindmillPower", PlayerVariables.WindmillPower);
		Debug.Log("Data Saved");
	}

	private void LoadData()
	{
		PlayerVariables.Score = PlayerPrefs.GetInt("Score", PlayerVariables.Score);
		PlayerVariables.PerClick = PlayerPrefs.GetInt("PerClick", PlayerVariables.PerClick);
		PlayerVariables.Warriors = PlayerPrefs.GetInt("Warriors", PlayerVariables.Warriors);
		PlayerVariables.DefensePercent = PlayerPrefs.GetFloat("DefensePercent", PlayerVariables.DefensePercent);
		PlayerVariables.GameVolume = PlayerPrefs.GetFloat("GameVolume", PlayerVariables.GameVolume);
		PlayerVariables.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", PlayerVariables.MusicVolume);
		PlayerVariables.WindmillPower = PlayerPrefs.GetInt("WindmillPower", PlayerVariables.WindmillPower);
		Debug.Log("Data Loaded");
	}
}
