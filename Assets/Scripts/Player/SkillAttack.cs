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
            Vector2 playerPosition = transform.position;
            Vector2 newEnemyPosition = new Vector2(playerPosition.x + 5f, enemy.transform.position.y);
            enemy.MoveToNewPosition(newEnemyPosition);
            enemy.TakeDamage(16);
        }
    }
}
