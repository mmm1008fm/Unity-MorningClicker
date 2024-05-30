using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResourceBank : MonoBehaviour
{
    public static ResourceBank Instance { get; private set; }

    public static event Action<int> OnScoreChanged;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    private int _score = 0;

    public int ScorePerClick = 1;
    public int ScorePerSecond = 0;
    public int Warriors = 0;
    public int Armor = 0;
    public int ScorePerClickCost = 50;
    public int ScorePerSecondCost = 10;
    public int WarriorCost = 50;
    public int ArmorCost = 10;
    public float SoundVolume = 0.5f;
    public float MusicVolume = 0.15f;

    public bool MagicScrollArtefact = false;
    public bool LavaStoneArtefact = false;
    public bool HeartOfForestArtefact = false;
    public bool ScarecrowHat = false;
    public bool HolyCup = false;

    [SerializeField] private int _autoSaveInterval;
    
    public float PerClickMultiplayer = 1f;
    public float PerSecondMultiplayer = 1f;
    public bool FirstTime = true;
    public bool ВесёлыйРежим;

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

        Load();
    }

    private void Start()
    {
        UniTask.Create(AutoSaveCycle);
    }

    private void Update()
    {
        float ps = 0f;
        float pc = 0f;

        if (LavaStoneArtefact)
        {
            ps += 0.5f;
        }

        if (MagicScrollArtefact)
        {
            pc += 0.5f;
        }

        if (HolyCup)
        {
            pc += 1f;
            ps += 1f;
        }

        PerClickMultiplayer = 1f + pc;
        PerSecondMultiplayer = 1f + ps;
    }

    private async UniTask AutoSaveCycle()
    {
        while (Application.isPlaying)
        {
            await UniTask.Delay(_autoSaveInterval);
            Save(Instance);
        }
    }

    public void Save(ResourceBank bank)
    {
        PlayerPrefs.SetInt("Score", bank.Score);
        PlayerPrefs.SetInt("ScorePerClick", bank.ScorePerClick);
        PlayerPrefs.SetInt("ScorePerSecond", bank.ScorePerSecond);
        PlayerPrefs.SetInt("Warriors", bank.Warriors);
        PlayerPrefs.SetInt("ScorePerClickCost", bank.ScorePerClickCost);
        PlayerPrefs.SetInt("ScorePerSecondCost", bank.ScorePerSecondCost);
        PlayerPrefs.SetInt("Armor", bank.Armor);
        PlayerPrefs.SetInt("WarriorCost", bank.WarriorCost);
        PlayerPrefs.SetInt("ArmorCost", bank.ArmorCost);
        PlayerPrefs.SetFloat("MusicVolume", bank.MusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", bank.SoundVolume);
        PlayerPrefs.SetInt("MagicScrollArtefact", bank.MagicScrollArtefact ? 1 : 0);
        PlayerPrefs.SetInt("LavaStoneArtefact", bank.LavaStoneArtefact ? 1 : 0);
        PlayerPrefs.SetInt("HeartOfForestArtefact", bank.HeartOfForestArtefact ? 1 : 0);
        PlayerPrefs.SetInt("ScarecrowHat", bank.ScarecrowHat ? 1 : 0);
        PlayerPrefs.SetInt("HolyCup", bank.HolyCup ? 1 : 0);
        PlayerPrefs.SetInt("FirstTime", bank.FirstTime ? 1 : 0);
        PlayerPrefs.SetInt("ВесёлыйРежим", bank.ВесёлыйРежим ? 1 : 0);
    }

    public void Load()
    {
        if (!Instance)
        {
            return;
        }

        Instance.Score = PlayerPrefs.GetInt("Score", 0);
        Instance.ScorePerClick = PlayerPrefs.GetInt("ScorePerClick", 1);
        Instance.ScorePerSecond = PlayerPrefs.GetInt("ScorePerSecond", 0);
        Instance.Warriors = PlayerPrefs.GetInt("Warriors", 0);
        Instance.ScorePerClickCost = PlayerPrefs.GetInt("ScorePerClickCost", 50);
        Instance.ScorePerSecondCost = PlayerPrefs.GetInt("ScorePerSecondCost", 10);
        Instance.Armor = PlayerPrefs.GetInt("Armor", 0);
        Instance.WarriorCost = PlayerPrefs.GetInt("WarriorCost", 50);
        Instance.ArmorCost = PlayerPrefs.GetInt("ArmorCost", 10);
        Instance.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.05f);
        Instance.SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.3f);
        Instance.MagicScrollArtefact = PlayerPrefs.GetInt("MagicScrollArtefact") == 1;
        Instance.LavaStoneArtefact = PlayerPrefs.GetInt("LavaStoneArtefact") == 1;
        Instance.HeartOfForestArtefact = PlayerPrefs.GetInt("HeartOfForestArtefact") == 1;
        Instance.ScarecrowHat = PlayerPrefs.GetInt("ScarecrowHat") == 1;
        Instance.HolyCup = PlayerPrefs.GetInt("HolyCup") == 1;
        Instance.FirstTime = PlayerPrefs.GetInt("FirstTime") == 1;
        Instance.ВесёлыйРежим = PlayerPrefs.GetInt("ВесёлыйРежим") == 1;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}