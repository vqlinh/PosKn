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

    public virtual void Move()
    {
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
    public virtual void Die()
    {
        // Sử dụng DOTween để di chuyển Enemy từ dưới lên theo hướng bên phải
        float duration = 1.5f;
        Vector2 targetPosition = new Vector2(transform.position.x + 10f, transform.position.y + 5f); // Điểm cuối cùng mà bạn muốn Enemy di chuyển đến
        transform.DOMove(targetPosition, duration).SetEase(Ease.Linear); // Có thể thay đổi Ease để có hiệu ứng di chuyển mượt mà hơn
    }

}