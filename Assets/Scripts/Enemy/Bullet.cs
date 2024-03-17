using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public RangedEnemy rangedEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.player))
        {
            collision.gameObject.GetComponent<Player>().DamagedState();
            if (rangedEnemy != null)
            {
                rangedEnemy.TakeDamagePlayer();
            }
            Destroy(this.gameObject);
        }
    }
    public void SetRangedEnemy(RangedEnemy enemy)
    {
        rangedEnemy = enemy;
    }
}
