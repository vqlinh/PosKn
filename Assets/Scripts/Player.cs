using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoBehaviour
{
    #region COOLDOWN
    [Header("CoolDown")]
    private bool isAttack = false;
    private bool isClick1 = false;
    public Image imgCoolDown1;
    public float coolDown1 = 2f;
    private bool isCoolDown1;
    [SerializeField] private float timeCoolDownAttack = 2f;
    private bool isShield = false;
    private bool isClick2 = false;
    public Image imgCoolDown2;
    public float coolDown2 = 3f;
    private bool isCoolDown2;
    [SerializeField] private float timeCoolDownShield = 3f;
    private bool isHealing = false;
    private bool isClick3 = false;
    public Image imgCoolDown3;
    public float coolDown3 = 8f;
    private bool isCoolDown3;
    [SerializeField] private float timeCoolDownHealing = 8f;
    //public Button btnSkill3;
    #endregion
    #region HEALTH
    [Header("HEALTH")]
    public HealthBar healthBar;
    private int currentHealth;
    [SerializeField] private int maxHealth = 100;
    #endregion
    [Header("MOVEMENT")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float disBack = 1f;
    [SerializeField] private float distanceMoveBack = 2f;
    [SerializeField] private float distanceAttack;
    //[SerializeField] private GameObject skillShield;

    private bool canTriggerDamagedState = true;
    Animator animator;
    private bool canMove = true;
    private bool canMoveBack = true;
    private bool hasAttacked = false;
    private bool isMoveBack = false;
    private bool hasMoveBack = false;
    private bool Shielding = false;
    private GameManager gameManager;
    public EnemySpawn enemySpawn;
    public GameObject skillAttack;
    private ButtonManager buttonManager;
    public SkillShield _skillShield;
    public DialogueTrigger dialogueTrigger;
    #region ENUM-STATE
    private PlayerState playerState;
    public enum PlayerState
    {
        Idle,
        Moving,
        SkillAttack,
        Damaged,
        NormalAttack
    }
    #endregion
    private void Start()
    {
        imgCoolDown1.fillAmount = 0;
        imgCoolDown2.fillAmount = 0;
        imgCoolDown3.fillAmount = 0;
        buttonManager = FindObjectOfType<ButtonManager>();
        skillAttack.SetActive(false);
        gameManager = GameManager.instance;
        animator = GetComponent<Animator>();

        playerState = PlayerState.Moving;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _skillShield.gameObject.SetActive(false);
        gameManager.txtMaxHeal.text = maxHealth.ToString();
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        gameManager.txtCurrentHeal.text = currentHealth.ToString();
        CheckDistanceForNormalAttack();
        CheckState();
        CoolDownSkill1();
        CoolDownSkill2();
        CoolDownSkill3();
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
            StartCoroutine(AttackCoolDown()); // doi 2 giay roi danh tiep
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
            Shielding = true;
            Shield();
            StartCoroutine(ShieldCoolDown());
        }
    }
    private IEnumerator ShieldCoolDown()
    {
        isShield = true;
        //animator.SetTrigger(Const.animSkillAttack);
        yield return new WaitForSeconds(timeCoolDownShield);
        isShield = false;
    }
    void Shield()
    {
        // hàm khiên ở đây
        Debug.Log("bat Shield");
        _skillShield.gameObject.SetActive(true);

        StartCoroutine(DestroyShield());
    }
    private IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(1f);
        _skillShield.ShieldDestroy();
        //yield return new WaitForSeconds(0.5f);
        //_skillShield.gameObject.SetActive(false);
        Shielding = false;

    }
    public void Hide()
    {
        _skillShield.gameObject.SetActive(false);

    }
    #endregion
    // -------------------------------------- máu hồi mà lớn hơn max thì k chạy hàm cooldown,(fix sau)-----------------------------------------------
    #region Skill_3
    public void Skillhealing() // click button
    {
        if (currentHealth < maxHealth)  // Kiểm tra điều kiện khi có thể click button
        {
            isClick3 = true;
            if (!isHealing)
            {
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
        }
    }

    private void CheckDistanceForNormalAttack()
    {
        float minDistance = float.MaxValue;
        for (int i = 0; i < enemySpawn.listEnemySpawn.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, enemySpawn.listEnemySpawn[i].transform.position);
            if (distance < minDistance) minDistance = distance;
        }
        if (minDistance <= distanceAttack && !hasAttacked) // danh tay truoc
        {
            hasAttacked = true;
            NormalAttackState();
        }
        else if (minDistance > distanceAttack) hasAttacked = false; // Đặt lại biến khi khoảng cách lớn hơn 2f
        if (minDistance <= distanceMoveBack && !hasMoveBack) // danh tay xong roi moi lui lai
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
        //playerState = PlayerState.Moving;
    }

    void MovingState()
    {
        animator.SetTrigger(Const.animMove);
        if (canMove) Move();
        canMoveBack = true;
    }

    void NormalAttackState()
    {
        animator.SetTrigger(Const.animNormalAttack);
        playerState = PlayerState.Moving;
    }

    public void DamagedState()
    {
        if (canTriggerDamagedState && canMoveBack && !isMoveBack && !Shielding)
        {
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

    public void TakeDamageFromEnemy(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.villageOld))
        {
            canMove = false;
            playerState = PlayerState.Idle;
            Invoke("Talk",1f);
            //TalkToPlayer();
        }
    }
    void Talk()
    {
        dialogueTrigger.TriggerDialouge();

    }
}

