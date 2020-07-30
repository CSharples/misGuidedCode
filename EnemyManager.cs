using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject player;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject bulletManager;
    public GameObject enemyBM;
    public GameObject enemySpawn;
    float time_since_spawn1;
    int next_spawn1;
    public int minSpawn1;
    public int maxSpawn1;
    float time_since_spawn2;
    int next_spawn2;
    public int minSpawn2;
    public int maxSpawn2;
    float time_since_spawn3;
    int next_spawn3;
    public int minSpawn3;
    public int maxSpawn3;
    private Vector3 spawnPosition;
    public int maxNumSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        next_spawn1 = 2;
        time_since_spawn1 = Time.time;
        next_spawn2 = 10;
        time_since_spawn2 = Time.time;
        next_spawn3 = 20;
        time_since_spawn3 = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time_since_spawn1 > next_spawn1)
        {
            Spawn(enemy1, maxNumSpawn+1);
            next_spawn1 = Random.Range(minSpawn1, maxSpawn1);
            time_since_spawn1 = Time.time;
        }
        if (Time.time - time_since_spawn2 > next_spawn2)
        {
            Spawn(enemy2, maxNumSpawn);
            next_spawn2 = Random.Range(minSpawn2, maxSpawn2);
            time_since_spawn2 = Time.time;
            if (maxSpawn2 > 4)
            {
                maxSpawn2 -= 1;
            }
            else
            {
                minSpawn2 = 1;
            }
        }
        if (Time.time - time_since_spawn3 > next_spawn3)
        {
            Spawn(enemy3, maxNumSpawn);
            next_spawn3 = Random.Range(minSpawn3, maxSpawn3);
            time_since_spawn3 = Time.time;
            if (maxSpawn2 > 10)
            {
                maxSpawn2 -= 1;
            }
            else
            {
                minSpawn2 = 5;
            }
        }
    }
    void Spawn(GameObject enemy, int rand)
    {
        int numEnemies = Random.Range(1, rand);
        for (int i = 0; i < numEnemies; i++) {
            float spawnY = Random.Range
                    ((Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y)
                    + (Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y * 0.2f),
                    Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y * 0.8f);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x
                + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x * 0.2f,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x * 0.8f);

            spawnPosition = new Vector3(spawnX, spawnY, 0);
            GameObject newEnemy = Instantiate(enemySpawn, spawnPosition, Quaternion.identity);
            newEnemy.GetComponent<EnemySpawnPoint>().player = player;
            newEnemy.GetComponent<EnemySpawnPoint>().enemyBM = enemyBM;
            newEnemy.GetComponent<EnemySpawnPoint>().enemy = enemy;
        }
    }
}
