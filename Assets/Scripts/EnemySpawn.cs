using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawn;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> listEnemySpawn = new List<GameObject>();
    public GameObject melee;
    public GameObject ranged;
    public Transform player;
    void Start()
    {
        GameObject melee1 = Instantiate(melee, new Vector2(player.transform.position.x+5f,player.transform.position.y), Quaternion.identity);
        GameObject ranged1 = Instantiate(ranged,new Vector2(player.transform.position.x + 10f, player.transform.position.y), Quaternion.identity);
        listEnemySpawn.Add(melee1);
        listEnemySpawn.Add(ranged1);
    }

    void Update()
    {
        listEnemySpawn.RemoveAll(enemy => enemy == null || !enemy.activeSelf);
    }
}
