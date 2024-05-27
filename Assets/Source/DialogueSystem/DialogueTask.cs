using Cysharp.Threading.Tasks;

public abstract class DialogueTask
{
    public string Text { get; private set; }
    public int RelevanceTimeMs { get; private set; }
    public bool Initialized { get; private set; }
    public DialogueObject[] DialogueObjects { get; private set; }
    public string[] Answers { get; private set; }

    public DialogueTask(string text, int relevanceTimeMs, DialogueObject[] actualDialogues, string[] answers)
    {
        Text = text;
        RelevanceTimeMs = relevanceTimeMs;
        DialogueObjects = actualDialogues;
        Answers = answers;
    }

    public async UniTaskVoid Initialize()
    {
        if (Initialized) return;
        Initialized = true;
        await UniTask.Delay(RelevanceTimeMs);
        Initialized = false;
    }

    public abstract void Activate();
}