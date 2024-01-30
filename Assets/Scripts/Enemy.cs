using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public virtual void Attack()
    {
        // Logic tấn công chung cho tất cả Enemy ở đây
        Debug.Log("Generic Attack");
    }

    public virtual void Move(Vector2 targetPosition)
    {
        // Logic di chuyển chung cho tất cả Enemy ở đây
    }

}