using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class BattleLogic : MonoBehaviour
{
    public int PlayerInitHealth;
    public int EnemyInitHealth;
    public int PlayerHealth;
    public int EnemyHealth;
    public int PlayerAttack;
    public int EnemyAttack;
    public float Rage;

    [SerializeField] private Image _playerHealthBar;
    [SerializeField] private Image _enemyHealthBar;
    [SerializeField] private Image _rageBar;
    [SerializeField] private Image _background;
    [SerializeField] private float _rageRecession;
    [SerializeField] private float _rageRecharge;
    [SerializeField] private RectTransform _enemiesParent;
    [SerializeField] private RectTransform _spawnPos;
    [SerializeField] private BattleResultWindow _battleResultWindow;
    [SerializeField] private int _armorCost;
    [SerializeField] private GameObject _tmpAward1;
    [SerializeField] private GameObject _tmpAward2;

    private List<GameObject> _enemies = new List<GameObject>();
    public bool IsEnd;
    private int _armorLooses;
    private AudioSource _screams;

    private void Awake()
    {
        BrefingInfo brefingInfo = BrefingTransfer.Info;

        PlayerInitHealth = ResourceBank.Instance.Armor;
        PlayerAttack = ResourceBank.Instance.Warriors;
        EnemyInitHealth = brefingInfo.EnemyArmor;
        EnemyAttack = brefingInfo.EnemyWarriors;
        _background.sprite = brefingInfo.Location.Background;

        for (int i = 0; i < brefingInfo.EnemyTypePrefabs.Length; i++)
        {
            for (int j = 0; j < Random.Range(1, 3); j++)
            {
                _enemies.Add(Instantiate(
                    brefingInfo.EnemyTypePrefabs[i],
                    _spawnPos.position + new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), 0f),
                    Quaternion.identity,
                    _enemiesParent
                    ));
            }
        }

        PlayerHealth = PlayerInitHealth;
        EnemyHealth = EnemyInitHealth;
        _rageBar.fillAmount = 0f;
    }

    private void Start()
    {
        PlayerAttackRoutine().Forget();
        EnemyAttackRoutine().Forget();
        _screams = SoundManager.Instance.Play("battle");
    }

    private void Update()
    {
        if (IsEnd)
        {
            return;
        }

        _rageBar.fillAmount = Rage;
        _playerHealthBar.fillAmount = (float)PlayerHealth / PlayerInitHealth;
        _enemyHealthBar.fillAmount = (float)EnemyHealth / EnemyInitHealth;
        Rage = Mathf.Clamp(Rage - 0.5f * _rageRecession * Time.deltaTime, 0f, 1f);

        if (Input.GetMouseButtonDown(0))
        {
            Rage += _rageRecharge;
        }
    }

    private async UniTask EnemyAttackRoutine()
    {
        while (Application.isPlaying)
        {
            if (PlayerHealth <= 0)
            {
                Defeat();
            }

            if (IsEnd)
            {
                return;
            }

            await UniTask.Delay(1000);

            PlayerHealth -= EnemyAttack;

            SoundManager.Instance.Play("sword");
            
            if (Random.Range(0, 100) <= Rage * 100)
            {
                ResourceBank.Instance.Armor -= EnemyAttack;
                ResourceBank.Instance.ArmorCost -= _armorCost * EnemyAttack;
                _armorLooses += EnemyAttack;

                if (ResourceBank.Instance.Armor < 0)
                {
                    ResourceBank.Instance.Armor = 0;
                }

                if (ResourceBank.Instance.ArmorCost < _armorCost)
                {
                    ResourceBank.Instance.ArmorCost = _armorCost;
                }
            }
        }
    }

    private async UniTask PlayerAttackRoutine()
    {
        while (Application.isPlaying)
        {
            if (EnemyHealth <= 0)
            {
                Win();
            }

            if (IsEnd)
            {
                return;
            }

            await UniTask.Delay(1000 - (int)(Rage * 500));
            EnemyHealth -= PlayerAttack;
            SoundManager.Instance.Play("sword");
        }
    }

    private void Win()
    {
        if (IsEnd)
        {
            return;
        }

        IsEnd = true;
        BrefingInfo brefingInfo = BrefingTransfer.Info;
        _tmpAward1.SetActive(true);
        _tmpAward2.SetActive(true);
        _battleResultWindow.SetWin(brefingInfo.ActualArtefact,
            $"Вы одержали победу над врагом! Мы понесли потери в {_armorLooses} брони");
        SoundManager.Instance.Play("win");
        _screams.Stop();
    }

    private void Defeat()
    {
        if (IsEnd)
        {
            return;
        }

        IsEnd = true;
        _tmpAward1.SetActive(false);
        _tmpAward2.SetActive(false);
        _battleResultWindow.SetLose($"Наши войска приняли решение отступить. Мы потеряли {_armorLooses} брони...");
        SoundManager.Instance.Play("defeat");
        _screams.Stop();
    }
}