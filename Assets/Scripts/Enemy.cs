using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyState currentState;
    [SerializeField] private float disBack = 1f;
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
        Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 reverseDirection = transform.right;
            Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack; // di chuyen ve sau voi khoang cach disBack
             StartCoroutine(MoveBack(newPosition));
            HandleNormalAttackState();

        }
    }
    private IEnumerator MoveBack(Vector2 targetPosition)
    {
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
    }
    void Attack()
    {

    }
}
