using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public DialogueCycle DialogueCycle { get; private set; }
    [SerializeField] private DialoguesContext[] _context;
    private bool _okokok;

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
            DialogueCycle = new DialogueCycle(_context, 10000, 120000, 9000);
            // DialogueCycle = new DialogueCycle(_context, 2000, 2000, 1000);
            DialogueCycle.StartCycle().Forget();
        }
    }

    private void Update()
    {
        for (int i = 0; i < DialogueCycle.DialogueTasks.Count; i++)
        {
            if (!DialogueCycle.DialogueTasks[i].Initialized && !DialogueCycle.DialogueTasks[i].Activated)
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
            _okokok = false;
        }
        else
        {
            SoundManager.Instance.Play("new_kveste");

            if (ResourceBank.Instance.ВесёлыйРежим)
            {
                if (_okokok)
                {
                    Notification.Instance.SetActive(true);
                    _okokok = true;
                }
            }
            else
            {
                if (!_okokok)
                {
                    Notification.Instance.SetActive(true);
                    _okokok = true;
                }
            }
        }
    }
}