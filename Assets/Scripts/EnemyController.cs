using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyController : Enemy
{
    public EnemyType enemyType;
    public enum EnemyType
    {
        Melee,
        Ranged
    }

    float disBack = 2f;
    private Animator animator;
    private bool canAttack = false;
    private Transform playerTransform;
    public float moveSpeed = 1f;
    public float meleeAttackDistance = 1f;
    public float rangedAttackDistance = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

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
        animator.SetTrigger(Const.meleeMove);
    }

    private void RangedAttack()
    {
        Debug.Log("ranged attack");
    }

    private void MeleeAttack()
    {
        Debug.Log("melee attack");
        animator.SetTrigger(Const.meleeAttack);
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

