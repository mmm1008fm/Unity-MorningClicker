using Cysharp.Threading.Tasks;

public abstract class DialogueTask
{
    public string Text { get; private set; }
    public int RelevanceTimeMs { get; private set; }
    public bool Initialized { get; private set; }
    public DialoguesContext[] Context { get; private set; }
    public string[] Answers { get; private set; }
    public bool Activated { get; protected set; }

    public DialogueTask(string text, int relevanceTimeMs, DialoguesContext[] actualDialogues, string[] answers)
    {
        Text = text;
        RelevanceTimeMs = relevanceTimeMs;
        Context = actualDialogues;
        Answers = answers;
    }

    public async UniTaskVoid Initialize()
    {
        if (Initialized) return;
        Initialized = true;
        await UniTask.Delay(RelevanceTimeMs);

        if (Activated)
        {
            return;
        }

        Initialized = false;
    }

    public virtual void Activate()
    {
        Activated = true;
    }
}