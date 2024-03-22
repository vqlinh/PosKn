using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public HealthBar healthBar;

    private int attack;
    bool canDamaged=false;
    public GameObject bullet;
    public float bulletForce;
    private Animator animator;
    public EnemyData enemyData;
    public float rangedAttackDistance = 5f;
    float rangedAttackTimer = 0f;
    public float rangedAttackInterval = 3f;
    int expAmount;
    public RangedState rangedState;
    public enum RangedState
    {
        Idle,
        Attack,
        Damaged
    }
    private void Awake()
    {
        expAmount = enemyData.exp;
        currentHealth = enemyData.health;
        attack = enemyData.attack;
        healthBar.SetMaxHealth(currentHealth);
        rangedState = RangedState.Idle;
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
        player = GameObject.FindObjectOfType<Player>();
    }
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        CheckRangedState();
        CheckDistancePlayer();

    }
    public void CheckDistancePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 1.2f && !canDamaged)
        {
            canDamaged = true;
            Damaged();
        }
        else if (distanceToPlayer > 1.2f) canDamaged = false;
        if (distanceToPlayer <= rangedAttackDistance)
        {
            rangedAttackTimer += Time.deltaTime;
            if (rangedAttackTimer >= rangedAttackInterval)
            {
                RangedAttack(); 
                rangedAttackTimer = 0f;
            }
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

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (currentHealth <= 0)
        {
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }
    public override void Die()
    {
        base.Die();
        ExpManager.Instance.AddExp(expAmount);
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
    }

    void Shoot()
    {
        GameObject fireBullet = Instantiate(bullet, new Vector2(transform.position.x - 0.5f, 0f), transform.rotation);
        Bullet bulletScript = fireBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetRangedEnemy(this);
        }
        Rigidbody2D rb = fireBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
    }
    #endregion

    public void TakeDamagePlayer()
    {
        player.TakeDamageEnemy(attack);
    }

    void Damaged()
    {
        TakeDamage(10);
        if (currentHealth > 0)
        {
            RangedDamaged();
            Move();
        }
    }

    public override void Move()
    {
        base.Move();
    }
}
