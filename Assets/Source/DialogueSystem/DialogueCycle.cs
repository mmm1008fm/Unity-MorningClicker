using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogueCycle
{
    public List<DialogueTask> DialogueTasks { get; private set; } = new List<DialogueTask>();
    public DialogueObject[] GiftDialogue { get; private set; }
    private int _giftActualDuration { get; set; }
    private int _minMsInterval { get; set; }
    private int _maxMsInterval { get; set; }

    public DialogueCycle(DialogueObject[] giftlDialogues, int minMsInterval, int maxMsInterval, int giftActualDuration)
    {
        GiftDialogue = giftlDialogues;
        _minMsInterval = minMsInterval;
        _maxMsInterval = maxMsInterval;
        _giftActualDuration = giftActualDuration;
    }

    public async UniTaskVoid StartCycle()
    {
        while (Application.isPlaying)
        {
            await UniTask.Delay(Random.Range(_minMsInterval, _maxMsInterval));
            var gift = new DialogueGift("Test Gift", _giftActualDuration, GiftDialogue, new string[] { "Yes", "No" });
            gift.Initialize().Forget();
            DialogueTasks.Add(gift);
        }
    }
}