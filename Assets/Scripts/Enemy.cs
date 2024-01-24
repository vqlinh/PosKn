using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyState currentState;
    public enum EnemyState
    {
        Idle,
        NormalAttack
    }

    private TypeEnemy typeEnemy;
    public enum TypeEnemy
    {
        HandHit,
        Farhit
    }
    void Start()
    {
        currentState = EnemyState.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState();
                break;
            
            case EnemyState.NormalAttack:
                HandleNormalAttackState();
                break;
        }
    }
    void HandleIdleState()
    {
        if (typeEnemy == TypeEnemy.HandHit) currentState = EnemyState.Idle;
        else if (typeEnemy==TypeEnemy.Farhit) currentState = EnemyState.Idle;
    }
    void HandleNormalAttackState()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
