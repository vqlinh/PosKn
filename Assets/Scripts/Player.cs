using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float disBack = 1f;
    [SerializeField] private float moveSpeed = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 reverseDirection = -transform.right;
            Vector2 newPosition = (Vector2)transform.position + reverseDirection * disBack;
            StartCoroutine(MoveBack(newPosition));
        }
    }

    private IEnumerator MoveBack(Vector2 targetPosition)
    {
        PlayerMove playerMove = GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.SetCanMove(false); // Ng?ng di chuy?n player

            float elapsedTime = 0f;
            float duration = 0.5f; // Th?i gian di chuy?n ng??c l?i
            Vector2 initialPosition = transform.position;

            while (elapsedTime < duration)
            {
                transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;

            playerMove.SetCanMove(true); // Cho phép di chuy?n player l?i
        }
    }
}
