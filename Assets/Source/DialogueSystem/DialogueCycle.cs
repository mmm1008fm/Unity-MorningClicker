using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogueCycle
{
    public List<DialogueTask> DialogueTasks { get; private set; } = new List<DialogueTask>();

    public async UniTaskVoid StartCycle()
    {
        while (Application.isPlaying)
        {
            await UniTask.Delay(3000);
            var gift = new DialogueGift("Test Gift", 2000);
            gift.Initialize().Forget();
            DialogueTasks.Add(gift);
        }
    }
}

public abstract class DialogueTask
{
    public string Text { get; private set; }
    public int RelevanceTimeMs { get; private set; }
    public bool Initialized { get; private set; }

    public DialogueTask(string text, int relevanceTimeMs)
    {
        Text = text;
        RelevanceTimeMs = relevanceTimeMs;
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

public class DialogueGift : DialogueTask
{
    public DialogueGift(string text, int relevanceTime) : base(text, relevanceTime) { }

    public override void Activate()
    {
        if (!Initialized)
        {
            return;
        }

        Debug.Log($"Активировали подарок: {Text}");
    }
}