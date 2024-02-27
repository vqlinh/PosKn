using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    float disBack = 2f;

    public virtual void Attack()
    {
        // Logic tấn công chung cho tất cả Enemy ở đây
        Debug.Log("Generic Attack");
    }
    public void DisBack()
    {
        disBack += 2f;

    }

    public virtual void Move()
    {
        StartCoroutine(MoveBack());

    }
    private IEnumerator MoveBack( )
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector2 initialPosition = transform.position;
        Vector2 newPosition = (Vector2)transform.position + (Vector2)transform.right * disBack;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(initialPosition, newPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = newPosition;
    }
    public virtual void Die()
    {
        float duration = 1.5f;
        Vector2 targetPosition = new Vector2(transform.position.x + 10f, transform.position.y + 5f); 
        transform.DOMove(targetPosition, duration).SetEase(Ease.Linear); 
    }
    private void Update()
    {
        Debug.Log(disBack);
    }

}