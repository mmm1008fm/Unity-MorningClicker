using System.Collections;
using UnityEngine;

public class SaveData : BaseInitializable
{
	[SerializeField] private float _secondsSaveInterval = 10f;
	
	public override void Initialize()
	{
		LoadData();
		StartCoroutine(SaveCycle(_secondsSaveInterval));
	}
	
	private IEnumerator SaveCycle(float secondsInterval)
	{
		while (Application.isPlaying)
		{
			SetData();
			yield return new WaitForSeconds(secondsInterval);
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
		Debug.Log("Data Loaded");
	}
}
