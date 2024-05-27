public class DialogueGift : DialogueTask
{
    public DialogueGift(string text, int relevanceTime, DialogueObject[] actualDialogues, string[] answers)
    : base(text, relevanceTime, actualDialogues, answers) { }

    public override void Activate()
    {
        if (!Initialized)
        {
            return;
        }

        ResourceBank.Instance.Score += ResourceBank.Instance.Score / 2;
    }
}