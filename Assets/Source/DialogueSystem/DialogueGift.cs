public class DialogueGift : DialogueTask
{
    public DialogueGift(string text, int relevanceTime, DialoguesContext[] actualDialogues, string[] answers)
    : base(text, relevanceTime, actualDialogues, answers) { }

    public override void Activate()
    {
        base.Activate();
        ResourceBank.Instance.Score += ResourceBank.Instance.Score / 2;
    }
}