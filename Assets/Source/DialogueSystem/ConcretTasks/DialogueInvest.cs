using UnityEngine;

public class DialogueInvest : DialogueTask
{
    public bool IsLuck { get; private set; }
    public DialogueInvest(string text, int relevanceTime, DialoguesContext[] actualDialogues, string[] answers)
    : base(text, relevanceTime, actualDialogues, answers) { }

    public override void Activate()
    {
        base.Activate();
        if (Random.Range(0, 100) <= 50)
        {
            ResourceBank.Instance.Score /= 2;
            IsLuck = false;
        }
        else
        {
            ResourceBank.Instance.Score *= 3;
            IsLuck = true;
        }
    }
}