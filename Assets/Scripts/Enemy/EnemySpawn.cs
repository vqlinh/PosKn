using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawn;

public class EnemySpawn : MonoBehaviour
{
    bool isSpawn = false;
    public GameObject melee;
    public GameObject ranged;
    private Transform player;
    public GameObject chief;
    public List<GameObject> listEnemySpawn = new List<GameObject>();

    void Start()
    {
        player= GameObject.FindGameObjectWithTag(Const.player).transform;
        GameObject melee1 = Instantiate(melee, new Vector2(player.transform.position.x + 10f, player.transform.position.y), Quaternion.identity);
        GameObject ranged1 = Instantiate(ranged, new Vector2(player.transform.position.x + 20f, player.transform.position.y), Quaternion.identity);
        listEnemySpawn.Add(melee1);
        listEnemySpawn.Add(ranged1);
    }

    void Update()
    {
        listEnemySpawn.RemoveAll(enemy => enemy == null || !enemy.activeSelf);
        if (listEnemySpawn.Count == 0) SpawnChief();
    }

    void SpawnChief()
    {
        if (!isSpawn)
        {
            Invoke("Load", 1f);
            isSpawn = true;
            Instantiate(chief, new Vector2(player.transform.position.x + 10f, player.transform.position.y), Quaternion.identity);
        }
    }

    void Load()
    {
        Loading.Instance.LoadingOpen();
    }
}
