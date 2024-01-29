using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Ranged
    }

    public EnemyType enemyType;
    public float meleeAttackDistance = 1f;
    public float rangedAttackDistance = 5f;
    [SerializeField] private float disBack = 2f;
    int count = 0;


    private bool canAttack = false;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (enemyType == EnemyType.Melee)
        {
            if (distanceToPlayer <= meleeAttackDistance && !canAttack)
            {
                canAttack = true;
                MeleeAttack();
                count++;
                if (count==2)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else if (distanceToPlayer> meleeAttackDistance)
            {
                canAttack = false;
            }
        }
        else if (enemyType == EnemyType.Ranged)
        {
            if (distanceToPlayer <= rangedAttackDistance && !canAttack)
            {
                canAttack = true;
                Debug.Log("Ranged attack");
                //RangedAttack();
            }
            else if (distanceToPlayer > rangedAttackDistance)
            {
                canAttack = false;
            }
        }
    }
    private void RangedAttack()
    {
        Vector2 reverseDirection = transform.right;
        Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack;
        StartCoroutine(MoveBack(newPosition));
    }

    private void MeleeAttack()
    {
        // Logic xử lý tấn công ở đây
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
}
