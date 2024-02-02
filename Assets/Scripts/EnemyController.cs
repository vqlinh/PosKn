using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    public HealthBar healthBar;
    public EnemyType enemyType;
    public enum EnemyType
    {
        Melee,
        Ranged
    }

    public MeleeState meleeState;
    public enum MeleeState
    {
        Idle,
        Moving,
        Attack,
        Damaged
    }

    public RangedState rangedState;
    public enum RangedState
    {
        Idle,
        Attack,
        Damaged
    }

    float disBack = 2f;
    private Animator animator;
    private bool canAttack = false;
    private Transform playerTransform;
    public float moveSpeed = 1f;
    public float meleeAttackDistance = 1f;
    public float rangedAttackDistance = 5f;

    public int startHealth = 50;
    private int currentHealth;


    private void Start()
    {
        currentHealth = startHealth;
        healthBar.SetMaxHealth(startHealth);

        meleeState = MeleeState.Idle;
        rangedState = RangedState.Idle; // de day ti lam tiep
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
    }
    public void TakeDamage(int amount)
    {
        Debug.Log("TakeDame");
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        Debug.Log("currentHealth : " + currentHealth);

        if (currentHealth<=0)
        {
            Die();
        }

    }
    void Die()
    {
        Debug.Log("Die");
    }


    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        CheckDistanceToPlayer();
        CheckMeleeState();
    }
    public void CheckMeleeState()
    {
        switch (meleeState)
        {
            case MeleeState.Idle:
                MeleeIdle();
                break;
            case MeleeState.Moving:
                MeleeMoving();
                break;
            case MeleeState.Damaged:
                MeleeDamaged();
                break;
            case MeleeState.Attack:
                MeleeAttack();
                break;
        }
    }
    #region State
    void MeleeIdle()
    {
        animator.SetTrigger(Const.meleeIdle);
    }
    void MeleeMoving()
    {
        animator.SetTrigger(Const.meleeMove);
    }
    void MeleeDamaged()
    {
        animator.SetTrigger(Const.meleeDamaged);
    }
    void MeleeAttack()
    {
        animator.SetTrigger(Const.meleeAttack);
    }
    #endregion


    public void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 1.2f) Damaged();
        if (enemyType == EnemyType.Melee)
        {
            if (distanceToPlayer <= 5f)
            {
                MoveToPlayer();
            }
            if (distanceToPlayer <= meleeAttackDistance && !canAttack)
            {
                canAttack = true;
                MeleeAttack();
            }
            else if (distanceToPlayer > meleeAttackDistance) canAttack = false;
        }
        else if (enemyType == EnemyType.Ranged)
        {
            if (distanceToPlayer <= rangedAttackDistance && !canAttack)
            {
                canAttack = true;
                Debug.Log("Ranged attack");
                RangedAttack();
            }
            else if (distanceToPlayer > rangedAttackDistance) canAttack = false;
        }
    }

    void Damaged()
    {
        Vector2 reverseDirection = transform.right;
        Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack;
        StartCoroutine(MoveBack(newPosition));
        if (enemyType == EnemyType.Melee) MeleeDamaged();
        else if (enemyType == EnemyType.Ranged)
        {
            // animation ranged Damaged
        }
    }

    private IEnumerator MoveBack(Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector2 initialPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    public void MoveToPlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        Debug.Log("enemy chay toi player");
        MeleeMoving();
    }

    private void RangedAttack()
    {
        Debug.Log("ranged attack");
    }


    public override void Attack()
    {
        base.Attack(); // Gọi Attack của lớp cha

    }

    public override void Move(Vector2 targetPosition)
    {
        base.Move(targetPosition); 
    }
}

