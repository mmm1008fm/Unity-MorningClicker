using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

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

    private int _score = 100000000;

    public int ScorePerClick = 1;
    public int ScorePerSecond = 0;
    public int Warriors = 0;
    public int Armor = 0;
    public int ScorePerClickCost = 50;
    public int ScorePerSecondCost = 20;
    public int WarriorCost = 50;
    public int ArmorCost = 10;
    public float SoundVolume = 0.75f;
    public float MusicVolume = 0.4f;

    public bool MagicScrollArtefact = false;
    public bool LavaStoneArtefact = false;
    public bool HeartOfForestArtefact = false;
    public bool ScarecrowHat = false;
    public bool HolyCup = false;

    [SerializeField] private int _autoSaveInterval;
    
    public float PerClickMultiplayer = 1f;
    public float PerSecondMultiplayer = 1f;
    public bool FirstTime = true;

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
    }

    public void Load()
    {
        if (!Instance)
        {
            return;
        }

        Instance.Score = PlayerPrefs.GetInt("Score", Instance.Score);
        Instance.ScorePerClick = PlayerPrefs.GetInt("ScorePerClick", Instance.ScorePerClick);
        Instance.ScorePerSecond = PlayerPrefs.GetInt("ScorePerSecond", Instance.ScorePerSecond);
        Instance.Warriors = PlayerPrefs.GetInt("Warriors", Instance.Warriors);
        Instance.ScorePerClickCost = PlayerPrefs.GetInt("ScorePerClickCost", Instance.ScorePerClickCost);
        Instance.ScorePerSecondCost = PlayerPrefs.GetInt("ScorePerSecondCost", Instance.ScorePerSecondCost);
        Instance.Armor = PlayerPrefs.GetInt("Armor", Instance.Armor);
        Instance.WarriorCost = PlayerPrefs.GetInt("WarriorCost", Instance.WarriorCost);
        Instance.ArmorCost = PlayerPrefs.GetInt("ArmorCost", Instance.ArmorCost);
        Instance.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", Instance.MusicVolume);
        Instance.SoundVolume = PlayerPrefs.GetFloat("SoundVolume", Instance.SoundVolume);
        Instance.MagicScrollArtefact = PlayerPrefs.GetInt("MagicScrollArtefact", Instance.MagicScrollArtefact ? 1 : 0) == 1;
        Instance.LavaStoneArtefact = PlayerPrefs.GetInt("LavaStoneArtefact", Instance.LavaStoneArtefact ? 1 : 0) == 1;
        Instance.HeartOfForestArtefact = PlayerPrefs.GetInt("HeartOfForestArtefact", Instance.HeartOfForestArtefact ? 1 : 0) == 1;
        Instance.ScarecrowHat = PlayerPrefs.GetInt("ScarecrowHat", Instance.ScarecrowHat ? 1 : 0) == 1;
        Instance.HolyCup = PlayerPrefs.GetInt("HolyCup", Instance.HolyCup ? 1 : 0) == 1;
        Instance.FirstTime = PlayerPrefs.GetInt("FirstTime", Instance.FirstTime ? 1 : 0) == 1;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            SceneSwitcher.QuitGame();
        #endif
    }
}