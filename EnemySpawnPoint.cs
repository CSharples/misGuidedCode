using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject enemyBM;
    public GameObject enemy;
    void Start()
    {
        StartCoroutine("spawnWait");
    }

    IEnumerator spawnWait(){
        yield return new WaitForSeconds(1f);
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy1>().player = player;
        newEnemy.GetComponent<Enemy1>().enemyBM = enemyBM;
        if(newEnemy.transform.childCount>0){
            newEnemy.GetComponent<Enemy1>().shield=true;
        }
        else{
            newEnemy.GetComponent<Enemy1>().shield=false;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
