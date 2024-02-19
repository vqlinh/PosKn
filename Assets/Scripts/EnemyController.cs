﻿using UnityEngine;
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
    private Player player;
    public float moveSpeed = 1f;
    public float meleeAttackDistance = 1f;
    public float rangedAttackDistance = 5f;

    private int currentHealth;
    public EnemyData enemyData;
    private int attack;

    public GameObject bullet;
    public float TimeBtwFire = 3f;
    public float bulletForce;
    private float timeBtwFire;


    private void Awake()
    {
        currentHealth = enemyData.health;
        attack = enemyData.attack;
        healthBar.SetMaxHealth(currentHealth);
        meleeState = MeleeState.Idle;
        rangedState = RangedState.Idle; // de day ti lam tiep
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
        player = GameObject.FindObjectOfType<Player>();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        this.gameObject.SetActive(false); 
    }


    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        CheckDistanceToPlayer();
        CheckMeleeState();
        CheckRangedState();
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
    public void CheckRangedState()
    {
        switch (rangedState)
        {
            case RangedState.Idle:
                RangedIdle();
                break;
            case RangedState.Damaged:
                RangedDamaged();
                break;
            case RangedState.Attack:
                RangedAttack();
                break;
        }
    }
    #region RangedState
    void RangedIdle()
    {
        animator.SetTrigger(Const.rangedIdle);
    }
    void RangedDamaged()
    {
        animator.SetTrigger(Const.rangedDamaged);
    }
    void RangedAttack()
    {
        animator.SetTrigger(Const.rangedAttack);
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (gameObject != null ) // Kiểm tra xem đối tượng có tồn tại không
            {
                Shoot();
                yield return new WaitForSeconds(3f);
            }
            else
            {
                // Đối tượng không tồn tại, thoát khỏi coroutine
                yield break;
            }
        }
    }

    void Shoot()
    {
        GameObject fireBullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rb = fireBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
    }
    #endregion
    #region MeleeState
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
        TakeDamegeFromPlayer(); 
        animator.SetTrigger(Const.meleeAttack);
    }
    #endregion


    public void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 1.2f)
        {
            Damaged();

        }
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
            if (distanceToPlayer <= rangedAttackDistance )
            {
                Debug.Log("distanceToPlayer <= rangedAttackDistance");
                //canAttack = true;
                RangedAttack();
            }
            //else if (distanceToPlayer > rangedAttackDistance) canAttack = false;
        }
    }
    void TakeDamegeFromPlayer() // khi chay ham nay thi Player se mat mau
    {
        player.TakeDamageFromEnemy(enemyData.attack);
    }

    void Damaged()
    {
        TakeDamage(10);
        Vector2 reverseDirection = transform.right;
        Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack;
        StartCoroutine(MoveBack(newPosition));
        if (enemyType == EnemyType.Melee) MeleeDamaged();
        else if (enemyType == EnemyType.Ranged)
        {
            RangedDamaged();
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
        MeleeMoving();
    }

    //private void RangedAttack()
    //{
    //    Debug.Log("RangedAttack");
    //    StartCoroutine(ShootRoutine());
    //}




    public override void Move(Vector2 targetPosition)
    {
        base.Move(targetPosition);
    }
}

