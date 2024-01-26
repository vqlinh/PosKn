using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [Header("CoolDown")]
    public Image imgCoolDown1;
    public float coolDown1 = 2f;
    private bool isCoolDown1;
    [SerializeField] private float timeCoolDownAttack = 2f;
    public Image imgCoolDown3;
    public float coolDown3 = 8f;
    private bool isCoolDown3;
    [SerializeField] private float timeCoolDownHealing = 8f;

    [Header("Health")]
    public HealthBar healthBar;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    [Header("/")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float disBack = 1f;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isHealing = false;
    [SerializeField] private float distanceAttack = 2f;

    Animator animator;
    private bool canMove = true;
    private bool canMoveBack = true;
    private bool hasAttacked = false;
    private bool isClick1 = false;
    private bool isClick3 = false;
    private GameManager gameManager;
    private Transform enemyTransform;

    private PlayerState currentState;
    public enum PlayerState
    {
        Idle,
        Moving,
        SkillAttack,
        Damaged,
        NormalAttack
    }

    private void Start()
    {
        enemyTransform = GameObject.FindGameObjectWithTag(Const.enemy).transform;
        Debug.Log("co Enemy");

        imgCoolDown3.fillAmount = 0;
        imgCoolDown1.fillAmount = 0;
        //isClick1 = true;
        // isClick3 = true;
        gameManager = GameManager.instance;
        animator = GetComponent<Animator>();
        currentState = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager.txtMaxHeal.text = maxHealth.ToString();
    }

    private void Update()
    {
        gameManager.txtCurrentHeal.text = currentHealth.ToString();
        CheckDistanceForNormalAttack();

        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdleState();
                break;
            case PlayerState.Moving:
                HandleMovingState();
                break;
            case PlayerState.SkillAttack:
                HandleSkillAttackState();
                break;
            case PlayerState.Damaged:
                HandleDamagedState();
                break;
            case PlayerState.NormalAttack:
                HandleNormalAttackState();
                break;
        }
        CoolDownSkill1();
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

    private void CheckDistanceForNormalAttack()
    {
        float distance = Vector2.Distance(transform.position, enemyTransform.position);
        if (distance <= distanceAttack && !hasAttacked)
        {
            Debug.Log("enemy trong vung attack");
            currentState = PlayerState.NormalAttack;
            //if (currentState == PlayerState.NormalAttack)
            //{
            //    Debug.Log("distanceAttack"+ distance);
            //    HandleNormalAttackState();
            //    hasAttacked = true; // Đánh chỉ một lần khi khoảng cách thỏa mãn
            //    currentState = PlayerState.Moving;
            //}
            if (currentState == PlayerState.Moving)
            {
                HandleDamagedState();
                TakeDamage(10);
            }
            if (currentState == PlayerState.SkillAttack)
            {
                HandleSkillAttackState();
                currentState = PlayerState.Moving;
            }
        }
        else if (distance > distanceAttack) hasAttacked = false; // Đặt lại biến khi khoảng cách lớn hơn 2f

    }
    // Các hàm xử lý cho từng trạng thái
    void HandleIdleState()
    {
        animator.SetTrigger(Const.animIdle);
        currentState = PlayerState.Moving;
    }

    void HandleMovingState()
    {
        animator.SetTrigger(Const.animMove);
        if (canMove) Move();
        canMoveBack = true;
    }

    void HandleNormalAttackState()
    {
        Debug.Log("NormalAttack");
        animator.SetTrigger(Const.animNormalAttack);
        currentState = PlayerState.Moving;
    }
    #region skill_1
    public void HandleSkillAttackState()
    {
        isClick1 = true;
        if (!isAttack)
        {
            Attack();
            StartCoroutine(AttackCoolDown()); // doi 2 giay roi danh tiep
            currentState = PlayerState.Moving;
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
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        float attackDuration = 0.4f;
        float elapsedTime = 0f;
        Vector2 attackStartPos = transform.position;
        Vector2 attackEndPos = transform.position + transform.right * 3f;
        while (elapsedTime < attackDuration)
        {
            transform.position = Vector2.Lerp(attackStartPos, attackEndPos, elapsedTime / attackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = attackEndPos;
    }
    #endregion
    #region Skill_3
    public void Skillhealing() // click button
    {
        isClick3 = true;
        if (!isHealing)
        {
            Healing();
            StartCoroutine(HealingCoolDown());
        }

    }
    public void Healing()
    {
        bool isHealPlus = true;
        if (currentHealth < maxHealth)
        {
            if (isHealPlus)
            {
                currentHealth += 30;
                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                    isClick3 = false; // mai lam not khi mau day se k chay hoi chieu mau nua
                }
            }
        }
        else isHealPlus = false;
    }
    private IEnumerator HealingCoolDown()
    {
        isHealing = true;
        yield return new WaitForSeconds(timeCoolDownHealing);
        isHealing = false;
    }
    #endregion

    void HandleDamagedState()
    {
        Debug.Log("dinh dame va bi lui lai");
        animator.SetTrigger(Const.animDamaged);
        Vector2 reverseDirection = -transform.right;
        Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack; // di chuyen ve sau voi khoang cach disBack
        if (canMoveBack) StartCoroutine(MoveBack(newPosition));
    }

    private IEnumerator MoveBack(Vector2 targetPosition)
    {
        canMove = false;
        float elapsedTime = 0f;
        float duration = 0.5f; // thoi gian di chuyen nguoc lai
        Vector2 initialPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        canMove = true;
    }

    public void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
