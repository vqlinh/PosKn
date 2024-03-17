using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.enemy))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Vector2 playerPos = transform.position;
            Vector2 newPow = new Vector2(playerPos.x + 5f, enemy.transform.position.y);
            enemy.NewPos(newPow);
            enemy.TakeDamage(16);
        }
    }
}
