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

    private List<GameObject> _enemies = new List<GameObject>();
    private bool _isEnd;

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
    }

    private void Update()
    {
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

            if (_isEnd)
            {
                return;
            }

            await UniTask.Delay(1000);
            PlayerHealth -= EnemyAttack;
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

            if (_isEnd)
            {
                return;
            }

            await UniTask.Delay(1000 - (int)(Rage * 500));
            EnemyHealth -= PlayerAttack;
        }
    }

    private void Win()
    {
        BrefingInfo brefingInfo = BrefingTransfer.Info;
        Debug.Log("Win");
        _isEnd = true;
        _battleResultWindow.SetWin(brefingInfo.ActualArtefact,
            $"Вы одержали победу над врагом: {brefingInfo.Location.Description}");
    }

    private void Defeat()
    {
        BrefingInfo brefingInfo = BrefingTransfer.Info;
        Debug.Log("Defeat");
        _isEnd = true;
        _battleResultWindow.SetLose($"Вы проиграли врагу: {brefingInfo.Location.Description}");
    }
}