using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    public HealthBar healthBar;

    private Transform playerTransform;
    private Player player;
    private Animator animator;
    public float rangedAttackDistance = 5f;
    private int currentHealth;
    public EnemyData enemyData;
    public GameObject bullet;
    public float TimeBtwFire = 3f;
    public float bulletForce;
    private float timeBtwFire;
    private int attack;

    public RangedState rangedState;
    public enum RangedState
    {
        Idle,
        Attack,
        Damaged
    }
    private void Awake()
    {
        currentHealth = enemyData.health;
        attack = enemyData.attack;
        healthBar.SetMaxHealth(currentHealth);
        rangedState = RangedState.Idle; // de day ti lam tiep
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
        player = GameObject.FindObjectOfType<Player>();
    }
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        CheckRangedState();
        CheckDistanceToPlayer();

    }
    public void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 1.2f) Damaged();
        if (distanceToPlayer <= rangedAttackDistance) RangedAttack();
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }
    public override void Die()
    {
        base.Die();
    }
    #region RangedState
    void RangedIdle()
    {
        animator.SetTrigger(Const.rangedIdle);
        Debug.Log("RangedIdle");
    }
    void RangedDamaged()
    {
        animator.SetTrigger(Const.rangedDamaged);
        Debug.Log("RangedDamaged");

    }
    void RangedAttack()
    {
        animator.SetTrigger(Const.rangedAttack);
        timeBtwFire -= Time.deltaTime;
        if (timeBtwFire < 0) Shoot();
    }

    void Shoot()
    {
        timeBtwFire = TimeBtwFire;
        GameObject fireBullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rb = fireBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
    }
    #endregion

    void TakeDamegeFromPlayer() // khi chay ham nay thi Player se mat mau
    {
        player.TakeDamageFromEnemy(enemyData.attack);
    }

    void Damaged()
    {
        TakeDamage(10);
        RangedDamaged();
        Move();
    }

    public override void Move()
    {
        base.Move();
        Debug.Log("MoveBack");
    }
}
