using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawn;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> listEnemySpawn = new List<GameObject>();
    public GameObject melee;
    public GameObject ranged;
    void Start()
    {
        GameObject melee1 = Instantiate(melee, transform.position, Quaternion.identity);
        Debug.Log("sinh ra melee" + melee1.transform.position);
        GameObject ranged1 = Instantiate(ranged,new Vector2(transform.position.x+3f,transform.position.y), Quaternion.identity);
        Debug.Log("sinh ra ranged" + ranged1.transform.position);

        listEnemySpawn.Add(melee1);
        listEnemySpawn.Add(ranged1);
    }

    void Update()
    {

    }
}
