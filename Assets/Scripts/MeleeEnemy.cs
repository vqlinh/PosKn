using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public HealthBar healthBar;

    private Transform playerTransform;
    private Player player;
    public float moveSpeed = 1f;
    public float meleeAttackDistance = 1f;
    private Animator animator;
    private bool canAttack = false;
    private int currentHealth; 
    private int attack;
    public EnemyData enemyData;


    public MeleeState meleeState;
    public enum MeleeState
    {
        Idle,
        Moving,
        Attack,
        Damaged
    }

    private void Awake()
    {
        currentHealth = enemyData.health;
        attack = enemyData.attack;
        healthBar.SetMaxHealth(currentHealth);
        meleeState = MeleeState.Idle;
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);
        CheckMeleeState();
        CheckDistanceToPlayer();
    }
    public void CheckMeleeState()
    {
        if (meleeState == MeleeState.Damaged) return;
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
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die(); // Nếu hết máu thì chạy hàm Die ngay tại đây
            return; // Kết thúc hàm TakeDamage ngay lập tức
        }
    }
    public override void Die()
    {
       base.Die();
    }
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

    void TakeDamegeFromPlayer() // khi chay ham nay thi Player se mat mau
    {
        player.TakeDamageFromEnemy(enemyData.attack);
    }
    public void MoveToPlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        MeleeMoving();
    }
    public void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        //if (distanceToPlayer <= 1.2f) Damaged();
        if (distanceToPlayer <= 5f) MoveToPlayer();
        if (distanceToPlayer <= meleeAttackDistance && !canAttack)
        {
            Damaged();
            canAttack = true;
            MeleeAttack();
        }
        else if (distanceToPlayer > meleeAttackDistance) canAttack = false;
    }
    void Damaged()
    {
        TakeDamage(10);
        MeleeDamaged();
        Move();
    }
    public override void Move()
    {
        base.Move();
    }
}
