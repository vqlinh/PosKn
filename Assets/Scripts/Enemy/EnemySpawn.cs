using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    bool isSpawn = false;
    public GameObject melee;
    public GameObject ranged;
    private Transform player;
    public GameObject chief;
    public List<GameObject> listEnemySpawn = new List<GameObject>();
    public int numberSpawn = 2;
    public float min;
    public float max;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.player).transform;
        SpawnEnemies();
    }

    void Update()
    {
        listEnemySpawn.RemoveAll(enemy => enemy == null || !enemy.activeSelf);
        if (listEnemySpawn.Count == 0) Chief();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberSpawn; i++)
        {
            GameObject randomEnemy = Random.Range(0, 2) == 0 ? melee : ranged;
            // Tính toán vị trí spawn của mỗi enemy
            float randomX = player.position.x + Random.Range(min, max);
            float randomY = player.position.y;
            Vector2 randomPosition = new Vector2(randomX, randomY);
            SpawnEnemy(randomEnemy, randomPosition);
        }
    }

    void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        listEnemySpawn.Add(enemy);
    }

    void Chief()
    {
        if (!isSpawn)
        {
            Invoke("Load", 1f);
            isSpawn = true;
            Instantiate(chief, player.position + new Vector3(10f, 0f, 0f), Quaternion.identity);
        }
    }

    void Load()
    {
        Loading.Instance.LoadingOpen();
    }
}
