using Cysharp.Threading.Tasks;

public abstract class DialogueTask
{
    public string Text { get; private set; }
    public int RelevanceTimeMs { get; private set; }
    public bool Initialized { get; private set; }
    public DialogueObject DialogueObject { get; private set; }

    public DialogueTask(string text, int relevanceTimeMs, DialogueObject actualDialogue)
    {
        Text = text;
        RelevanceTimeMs = relevanceTimeMs;
        DialogueObject = actualDialogue;
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