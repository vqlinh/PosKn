using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float disBack = 1f;
    [SerializeField] private float speed = 1f;
    private bool canMove = true;
    private bool canMoveBack= true;
    Animator animator;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public HealthBar healthBar;
    [SerializeField] private bool isAttack=false;
    [SerializeField] private float timeCoolDownAttack = 2f;


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
        animator = GetComponent<Animator>();
        currentState = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GameManager.instance.txtMaxHeal.text = maxHealth.ToString();
    }

    private void Update()
    {
        GameManager.instance.txtCurrentHeal.text = currentHealth.ToString();

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
    }
    // Các hàm xử lý cho từng trạng thái
    void HandleIdleState()
    {
        animator.SetTrigger(Const.animIdle);
        currentState= PlayerState.Moving;
    }

    void HandleMovingState()
    {
        animator.SetTrigger(Const.animMove);
        if (canMove) Move();
        canMoveBack = true;
    }
    void HandleNormalAttackState()
    {
        animator.SetTrigger(Const.animNormalAttack);
        currentState = PlayerState.Moving;
    }

    public void HandleSkillAttackState()
    {

        if (!isAttack)
        {
            Attack();
            StartCoroutine(AttackCoolDown());
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

    void HandleDamagedState()
    {
        animator.SetTrigger(Const.animDamaged);
        Vector2 reverseDirection = -transform.right;
        Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack; // di chuyen ve sau voi khoang cach disBack
        if (canMoveBack) StartCoroutine(MoveBack(newPosition));
    }

    private IEnumerator MoveBack(Vector2 targetPosition)
    {
        canMove = false;
        //animator.SetBool(Const.animMove, false);
        //animator.SetBool(Const.animDamaged, true);
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
        //animator.SetBool(Const.animDamaged, false );
        //animator.SetBool(Const.animMove, true);
    }
    // Hàm để chuyển đổi trạng thái

    public void Move()
    {
        //animator.SetBool(Const.animMove, true);
        transform.Translate(Vector3.right * speed * Time.deltaTime); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Const.enemy)) HandleDamagedState();
        // chay ham giam health
        TakeDamage(10);
        Debug.Log("TakeDamage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.enemy))
        {
            HandleNormalAttackState();
            Debug.Log("Chem chet");

        }
                
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth); 
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
}
