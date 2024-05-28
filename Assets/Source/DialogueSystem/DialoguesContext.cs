using UnityEngine;

[System.Serializable]
public class DialoguesContext
{
	[field: SerializeField] public DialogueObject Start { get; private set; }
	[field: SerializeField] public DialogueObject End { get; private set; }
	[field: SerializeField] public DialogueObject[] Answers { get; private set; }
}