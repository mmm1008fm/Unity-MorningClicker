using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : BaseInitializable
{
	[SerializeField] private float _autoSaveInterval = 10f;
	[SerializeField] private bool _resetProgress = false;
	private static bool _isAlreadyReset;
	private static SaveData singleton;
	
	public override void Initialize()
	{
		SceneManager.sceneUnloaded += SceneUnload;
		
		if (!singleton)
		{
			DontDestroyOnLoad(gameObject);
			singleton = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
		if (_resetProgress && !_isAlreadyReset)
		{
			_isAlreadyReset = true;
			PlayerPrefs.DeleteAll();
		}
		else
		{
			LoadData(); // Заного грузит при переходе с другой сцены (решил путём сохранения при выходе со сцены)
		}
		
		StartCoroutine(SaveCycle(_autoSaveInterval));
	}

	private void SceneUnload(Scene arg0)
	{
		SetData();
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
	}
}