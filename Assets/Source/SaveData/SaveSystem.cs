using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	private static readonly BinaryFormatter _formatter;
	private static string _saveFilePath;

	static SaveSystem()
	{
		_formatter = new BinaryFormatter();
		_saveFilePath = Path.Combine(Application.persistentDataPath, "storage.data");
	}

	public static void DeleteSaveFile() => File.Delete(_saveFilePath);

	public static void SaveClickerData(PlayerVariables data)
	{
		var stream = new FileStream(_saveFilePath, FileMode.Create);

		_formatter.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerVariables LoadClickerData()
	{
		if (File.Exists(_saveFilePath))
		{
			Debug.Log($"Save file was found in {_saveFilePath}");

			var stream = new FileStream(_saveFilePath, FileMode.Open);
			PlayerVariables data;

			try
			{
				data = _formatter.Deserialize(stream) as PlayerVariables;
			}
			catch
			{
				Debug.LogError($"Save file is corrupted in {_saveFilePath}");
				stream.Close();
				File.Delete(_saveFilePath);
				return null;
			}

			stream.Close();
			return data;
		}

		Debug.LogWarning($"Save file not found in {_saveFilePath}");
		return null;
	}
}