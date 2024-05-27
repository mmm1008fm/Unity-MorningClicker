using UnityEngine;

public class DialogueInvest : DialogueTask
{
    public DialogueInvest(string text, int relevanceTime, DialogueObject actualDialogue)
    : base(text, relevanceTime, actualDialogue) { }

    public override void Activate()
    {
        if (!Initialized)
        {
            return;
        }

        ResourceBank.Instance.Score += ResourceBank.Instance.Score / 2;
    }
}