using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue")]
public class DialogueObject : ScriptableObject
{
	public string PersonName = "Name";
	public Sprite PersonAwatar;
	public string[] Content;
}