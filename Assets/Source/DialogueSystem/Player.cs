using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public DialogueCycle DialogueCycle { get; private set; }
    [SerializeField] private DialogueObject _giftDialogue;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (DialogueCycle == null)
        {
            DialogueCycle = new DialogueCycle(_giftDialogue, 10000, 120000, 60000);
            DialogueCycle.StartCycle().Forget();
        }
    }

    private void Update()
    {
        for (int i = 0; i < DialogueCycle.DialogueTasks.Count; i++)
        {
            if (!DialogueCycle.DialogueTasks[i].Initialized)
            {
                DialogueCycle.DialogueTasks.Remove(DialogueCycle.DialogueTasks[i]);
            }
        }

        if (SceneManager.GetActiveScene().name != "Main")
        {
            return;
        }

        if (DialogueCycle.DialogueTasks.Count == 0)
        {
            Notification.Instance.SetActive(false);
        }
        else
        {
            Notification.Instance.SetActive(true);
        }
    }
}