using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class ResourceBank : MonoBehaviour
{
    public static ResourceBank Instance { get; private set; }

    public static UnityAction<int> ScoreChanged;
    public static UnityAction<int> ScorePerClickChanged;
    public static UnityAction<int> ScorePerSecondChanged;
    public static UnityAction<int> WarriorsCountChanged;
    public static UnityAction<int> ScorePerClickCostChanged;
    public static UnityAction<int> ScorePerSecondCostChanged;
    public static UnityAction<float> DefensePercentChanged;
    public static UnityAction<int> ArmorCostChanged;
    public static UnityAction<int> WarriorCostChanged;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreChanged?.Invoke(value);
        }
    }
    private int _score = 0;

    public int ScorePerClick
    {
        get => _scorePerClick;
        set
        {
            _scorePerClick = value;
            ScorePerClickChanged?.Invoke(value);
        }
    }
    private int _scorePerClick = 1;

    public int ScorePerSecond
    {
        get => _scorePerSecond;
        set
        {
            _scorePerSecond = value;
            ScorePerSecondChanged?.Invoke(value);
        }
    }
    private int _scorePerSecond = 0;

    public int WarriorsCount
    {
        get => _warriorsCount;
        set
        {
            _warriorsCount = value;
            WarriorsCountChanged?.Invoke(value);
        }
    }
    private int _warriorsCount = 0;

    public int ScorePerClickCost
    {
        get => _scorePerClickCost;
        set
        {
            _scorePerClickCost = value;
            ScorePerClickCostChanged?.Invoke(value);
        }
    }
    private int _scorePerClickCost = 50;

    public int ScorePerSecondCost
    {
        get => _scorePerSecondCost;
        set
        {
            _scorePerSecondCost = value;
            ScorePerSecondCostChanged?.Invoke(value);
        }
    }
    private int _scorePerSecondCost = 50;

    public float DefensePercent
    {
        get => _defensePercent;
        set
        {
            _defensePercent = value;
            DefensePercentChanged?.Invoke(value);
        }
    }
    private float _defensePercent = 0f;

    public int WarriorCost
    {
        get => _warriorCost;
        set
        {
            _warriorCost = value;
            WarriorCostChanged?.Invoke(value);
        }
    }
    private int _warriorCost = 50;

    public int ArmorCost
    {
        get => _armorCost;
        set
        {
            _armorCost = value;
            ArmorCostChanged?.Invoke(value);
        }
    }
    private int _armorCost;

    [SerializeField] private int _autoSaveInterval;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Load();
        UniTask.Create(AutoSaveCycle);
    }

    private async UniTask AutoSaveCycle()
    {
        while (Application.isPlaying)
        {
            await UniTask.Delay(_autoSaveInterval);
            Save(Instance);
        }
    }

    public static void Save(ResourceBank bank)
    {
        PlayerPrefs.SetInt("Score", bank.Score);
        PlayerPrefs.SetInt("ScorePerClick", bank.ScorePerClick);
        PlayerPrefs.SetInt("ScorePerSecond", bank.ScorePerSecond);
        PlayerPrefs.SetInt("WarriorsCount", bank.WarriorsCount);
        PlayerPrefs.SetInt("ScorePerClickCost", bank.ScorePerClickCost);
        PlayerPrefs.SetInt("ScorePerSecondCost", bank.ScorePerSecondCost);
        PlayerPrefs.SetFloat("DefensePercent", bank.DefensePercent);
    }

    public static void Load()
    {
        if (!Instance)
        {
            return;
        }

        Instance.Score = PlayerPrefs.GetInt("Score", Instance.Score);
        Instance.ScorePerClick = PlayerPrefs.GetInt("ScorePerClick", Instance.ScorePerClick);
        Instance.ScorePerSecond = PlayerPrefs.GetInt("ScorePerSecond", Instance.ScorePerSecond);
        Instance.WarriorsCount = PlayerPrefs.GetInt("WarriorsCount", Instance.WarriorsCount);
        Instance.ScorePerClickCost = PlayerPrefs.GetInt("ScorePerClickCost", Instance.ScorePerClickCost);
        Instance.ScorePerSecondCost = PlayerPrefs.GetInt("ScorePerSecondCost", Instance.ScorePerSecondCost);
        Instance.DefensePercent = PlayerPrefs.GetFloat("DefensePercent", Instance.DefensePercent);
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteAll();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            SceneSwitcher.QuitGame();
        #endif
    }
}