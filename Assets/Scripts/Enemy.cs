using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool hasAttacked = false;
    public float distanceAttack = 1f;

    private EnemyType enemyType;
    private EnemyState currentState;
    private Transform playerTransform;
    [SerializeField] private float disBack = 2f;
    public enum EnemyState
    {
        Idle,
        NormalAttack
    }

    public enum EnemyType
    {
        Melee,
        Ranged
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
    }

    void Update()
    {
        CheckDistanceForNormalAttack();
  
    }
    private void CheckDistanceForNormalAttack()
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        GameObject playerObject = GameObject.FindGameObjectWithTag(Const.player);
        if (playerObject != null)
        {
            Vector2 playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
            float distance = Vector2.Distance(enemyPosition, playerPosition);

            if (distance <= distanceAttack && !hasAttacked)
            {
                Debug.Log("khoang cach enemy nho hon 1f");
                Vector2 reverseDirection = transform.right;
                Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack; // di chuyen ve sau voi khoang cach disBack
                StartCoroutine(MoveBack(newPosition));
                hasAttacked = true; // Đánh chỉ một lần khi khoảng cách thỏa mãn
  
            }
            else if (distance > distanceAttack)
            {
                hasAttacked = false; // Đặt lại biến khi khoảng cách lớn hơn 2f
            }
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

}
