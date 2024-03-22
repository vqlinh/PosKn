using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoBehaviour
{
    #region COOLDOWN
    [Header("CoolDown")]
    private bool isCoolDown1;
    private Image imgCoolDown1;
    public float coolDown1 = 2f;
    private bool isAttack = false;
    private bool isClick1 = false;
    [SerializeField] private float timeCoolDownAttack = 2f;

    private bool isCoolDown2;
    private Image imgCoolDown2;
    public float coolDown2 = 3f;
    private bool isShield = false;
    private bool isClick2 = false;
    [SerializeField] private float timeCoolDownShield = 3f;

    private bool isCoolDown3;
    private Image imgCoolDown3;
    public float coolDown3 = 8f;
    private bool isClick3 = false;
    private bool isHealing = false;
    [SerializeField] private float timeCoolDownHealing = 8f;
    #endregion
    #region HEALTH
    [Header("HEALTH & Exp")]
    public int maxExp;
    public int currentExp;
    public int currentLevel;
    private int currentHealth;
    private HealthBar expBar;
    private HealthBar healthBar;
    private TextMeshProUGUI level;
    [SerializeField] private int maxHealth;
    #endregion
    [Header("MOVEMENT")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float disBack = 1f;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float distanceMoveBack = 2f;

    Animator animator;
    private bool isFinish=false;
    public bool canMove = true;
    private bool Shielding = false;
    private bool canMoveBack = true;
    private bool isMoveBack = false;
    private bool hasAttacked = false;
    private bool hasMoveBack = false;
    private bool canTriggerDamagedState = true;

    public PlayerData playerData;
    private EnemySpawn enemySpawn;
    private GameObject skillAttack;
    private SkillShield _skillShield;
    private ButtonManager buttonManager;
    private DialogueTrigger dialogueTrigger;
    private TextMeshProUGUI txtMaxHeal;
    private TextMeshProUGUI txtCurrentHeal;
    private UiManager uiManager;
    #region ENUM-STATE
    private PlayerState playerState;
    public enum PlayerState
    {
        Idle,
        Moving,
        SkillAttack,
        Damaged,
        NormalAttack,
        Die
    }
    #endregion
    private void Awake()
    {
        LoadData();
        skillAttack = GameObject.Find("attack");
        expBar = GameObject.Find("ExpBar").GetComponent<HealthBar>();
        imgCoolDown1 = GameObject.Find("Image1").GetComponent<Image>();
        imgCoolDown2 = GameObject.Find("Image2").GetComponent<Image>();
        imgCoolDown3 = GameObject.Find("Image3").GetComponent<Image>();
        level = GameObject.Find("TxtLevel").GetComponent<TextMeshProUGUI>();
        _skillShield = GameObject.Find("Shield").GetComponent<SkillShield>();
        enemySpawn = GameObject.Find("SpawnEnemy").GetComponent<EnemySpawn>();
        healthBar = GameObject.Find("HealthBarPlayer").GetComponent<HealthBar>();
        txtMaxHeal = GameObject.Find("txtMaxheal").GetComponent<TextMeshProUGUI>();
        uiManager = GameObject.Find("PanelStageComplete").GetComponent<UiManager>();
        txtCurrentHeal = GameObject.Find("txtCurrentHeal").GetComponent<TextMeshProUGUI>();
        dialogueTrigger = GameObject.Find("DialogueBox").GetComponent<DialogueTrigger>();
    }
    private void Start()
    {

        txtMaxHeal.text = maxHealth.ToString();
        txtCurrentHeal.text = currentHealth.ToString();
        imgCoolDown1.fillAmount = 0;
        imgCoolDown2.fillAmount = 0;
        imgCoolDown3.fillAmount = 0;
        skillAttack.SetActive(false);

        playerState = PlayerState.Moving;
        expBar.SetMaxHealth(maxExp);
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        _skillShield.gameObject.SetActive(false);
        buttonManager = FindObjectOfType<ButtonManager>();
    }

    private void Update()
    {
        CheckState();
        CoolDownSkill1();
        CoolDownSkill2();
        CoolDownSkill3();
        expBar.SetHealth(currentExp);
        CheckDistanceNormalAttack();
        healthBar.SetHealth(currentHealth);
        level.text = currentLevel.ToString();
        txtCurrentHeal.text = currentHealth.ToString();
    }
    #region CoolDown_1
    public void CoolDownSkill1()
    {

        if (isClick1 && isCoolDown1 == false)
        {
            isCoolDown1 = true;
            imgCoolDown1.fillAmount = 1;
        }

        if (isCoolDown1)
        {
            imgCoolDown1.fillAmount -= 1 / coolDown1 * Time.deltaTime;
            if (imgCoolDown1.fillAmount <= 0)
            {
                imgCoolDown1.fillAmount = 0;
                isCoolDown1 = false;
                isClick1 = false;
            }
        }
    }
    #endregion    
    #region CoolDown_2
    public void CoolDownSkill2()
    {

        if (isClick2 && isCoolDown2 == false)
        {
            isCoolDown2 = true;
            imgCoolDown2.fillAmount = 1;
        }

        if (isCoolDown2)
        {
            imgCoolDown2.fillAmount -= 1 / coolDown2 * Time.deltaTime;
            if (imgCoolDown2.fillAmount <= 0)
            {
                imgCoolDown2.fillAmount = 0;
                isCoolDown2 = false;
                isClick2 = false;
            }
        }
    }
    #endregion
    #region CoolDown_3
    public void CoolDownSkill3()
    {
        if (isClick3 && isCoolDown3 == false)
        {
            isCoolDown3 = true;
            imgCoolDown3.fillAmount = 1;
        }
        if (isCoolDown3)
        {
            imgCoolDown3.fillAmount -= 1 / coolDown3 * Time.deltaTime;
            if (imgCoolDown3.fillAmount <= 0)
            {
                imgCoolDown3.fillAmount = 0;
                isCoolDown3 = false;
                isClick3 = false;
            }
        }
    }
    #endregion
    #region skill_1
    public void SkillAttackState()
    {
        canTriggerDamagedState = false;
        isClick1 = true;
        if (!isAttack)
        {
            skillAttack.SetActive(true);

            Attack();
            StartCoroutine(AttackCoolDown()); 
            playerState = PlayerState.Moving;

        }
    }

    private IEnumerator AttackCoolDown()
    {
        isAttack = true;
        animator.SetTrigger(Const.animSkillAttack);
        yield return new WaitForSeconds(timeCoolDownAttack);
        isAttack = false;
    }

    public void Attack()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxSkillAttack);
        transform.DOMove(transform.position + transform.right * 3f, 0.4f).SetEase(Ease.Linear).OnComplete(() =>
        {
            skillAttack.SetActive(false);
            canTriggerDamagedState = true;
        });
    }
    #endregion
    #region Skill_2
    public void SkillShield()
    {
        isClick2 = true;
        if (!isShield)
        {
            AudioManager.Instance.PlaySfx(SoundName.SfxShield);
            Shielding = true;
            Shield();
            StartCoroutine(ShieldCoolDown());
        }
    }
    private IEnumerator ShieldCoolDown()
    {
        isShield = true;
        yield return new WaitForSeconds(timeCoolDownShield);
        isShield = false;
    }
    void Shield()
    {
        _skillShield.gameObject.SetActive(true);
        StartCoroutine(DestroyShield());
    }
    private IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(1f);
        _skillShield.ShieldDestroy();
        Shielding = false;

    }
    public void Hide()
    {
        _skillShield.gameObject.SetActive(false);

    }
    #endregion
    #region Skill_3
    public void Skillhealing()
    {
        if (currentHealth < maxHealth) 
        {
            isClick3 = true;
            if (!isHealing)
            {
                AudioManager.Instance.PlaySfx(SoundName.Sfxhealing);
                Healing();
                StartCoroutine(HealingCoolDown());
            }
        }

    }
    public void Healing()
    {
        currentHealth += 10;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            isClick3 = false;
        }
    }
    private IEnumerator HealingCoolDown()
    {
        isHealing = true;
        yield return new WaitForSeconds(timeCoolDownHealing);
        isHealing = false;
    }
    #endregion
    private void CheckState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                IdleState();
                break;
            case PlayerState.Moving:
                MovingState();
                break;
            case PlayerState.SkillAttack:
                SkillAttackState();
                break;
            case PlayerState.Damaged:
                DamagedState();
                break;
            case PlayerState.NormalAttack:
                NormalAttackState();
                break;
            case PlayerState.Die:
                Die();
                break;
        }
    }

    private void CheckDistanceNormalAttack()
    {
        float minDistance = float.MaxValue;
        for (int i = 0; i < enemySpawn.listEnemySpawn.Count; i++)
        {
            if (enemySpawn.listEnemySpawn[i] != null)
            {
                float distance = Vector2.Distance(transform.position, enemySpawn.listEnemySpawn[i].transform.position);
                if (distance < minDistance) minDistance = distance;
            }
        }
        if (minDistance <= distanceAttack && !hasAttacked)
        {
            hasAttacked = true;
            NormalAttackState();
        }
        else if (minDistance > distanceAttack) hasAttacked = false;
        if (minDistance <= distanceMoveBack && !hasMoveBack)
        {
            hasMoveBack = true;
            DamagedState();
        }
        else if (minDistance > distanceMoveBack) hasMoveBack = false;
    }
    #region FUNC-STATE
    void IdleState()
    {
        animator.SetTrigger(Const.animIdle);
        Debug.Log("IdleState");
    }

    void MovingState()
    {
        animator.SetTrigger(Const.animMove);
        if (canMove) Move();
        canMoveBack = true;
    }

    void NormalAttackState()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxNormalAttack);
        animator.SetTrigger(Const.animNormalAttack);
        playerState = PlayerState.Moving;
    }

    public void DamagedState()
    {
        if (canTriggerDamagedState && canMoveBack && !isMoveBack && !Shielding)
        {
            AudioManager.Instance.PlaySfx(SoundName.SfxDamaged);

            buttonManager.DisableButtons();
            MoveBack();
        }
    }
    #endregion
    private void MoveBack()
    {
        animator.SetTrigger(Const.animDamaged);
        Vector2 newPosition = (Vector2)transform.position - (Vector2)transform.right * disBack;
        isMoveBack = true;
        canMove = false;
        transform.DOMove(newPosition, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            canMove = true;
            isMoveBack = false;
            buttonManager.EnableButtons();
        });
    }

    public void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void TakeDamageEnemy(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            canTriggerDamagedState=false;
            if(!isFinish) Invoke("PanelDie", 1f);
            Die();  
        }

        healthBar.SetHealth(currentHealth);
    }
    void Die()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxLose);

        animator.SetTrigger(Const.animDie);
        playerState = PlayerState.Die;
        canMove = false;
  
    }
    void PanelDie()
    {
        isFinish = true;
        uiManager.PanelFadeIn();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.chief))
        {
            canMove = false;
            playerState = PlayerState.Idle;
            Invoke("Talk", 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.coin)) PickUpCoin(collision.gameObject);
    }

    private void PickUpCoin(GameObject coin)
    {
        coin.transform.DOMove(transform.position, 0.2f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                coin.transform.DOScale(Vector3.zero, 0.1f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        GameManager.Instance.coin += 10;
                        GameManager.Instance.SaveCoin();
                        Destroy(coin);
                    });
            });
    }

    void Talk()
    {
        dialogueTrigger.BoxDialogue();
    }

    private void OnEnable()
    {
        ExpManager.Instance.OnExpChange += HandlerExpChange;
    }
    private void OnDisable()
    {
        ExpManager.Instance.OnExpChange -= HandlerExpChange;
    }

    private void HandlerExpChange(int newExp)
    {
        currentExp += newExp;
        if (currentExp >= maxExp) LevelUp();
        SaveData();
    }
    void LevelUp()
    {
        maxHealth += 10;
        currentLevel++;
        currentExp = currentExp - maxExp;
        maxExp += 100;
        SaveData();
    }

    public void SaveData()
    {
        playerData.maxExp = maxExp;
        playerData.currentExp = currentExp;
        playerData.currentLevel = currentLevel;
        playerData.maxHealth = maxHealth;
    }
    public void LoadData()
    {
        if (playerData == null)
            playerData = ScriptableObject.CreateInstance<PlayerData>();

        if (playerData != null)
        {
            maxExp=playerData.maxExp;
            currentExp = playerData.currentExp;
            currentLevel = playerData.currentLevel;
            maxHealth = playerData.maxHealth;
            currentHealth = maxHealth;
        }
    }
}

