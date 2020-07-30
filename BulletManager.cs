using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bullets = new List<GameObject>();
    public GameObject player; 
    public GameObject bullet;
    public Sprite homingSprite;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Vector3 playerPos = player.transform.position;
        foreach (var bullet in bullets)
        {
            if (bullet.GetComponent<Bullet>())
            {
                bullet.GetComponent<Bullet>().Home(player);
            }
            else if (bullet.GetComponent<EnemyBullet>())
            {
                bullet.GetComponent<EnemyBullet>().Move();
            }
        }
        
    }
    public void Fire(GameObject gunner)
    {
        GameObject newBullet = Instantiate(bullet, gunner.transform.position+(gunner.transform.up*offset), gunner.transform.rotation);
        newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, 0);
        if (newBullet.GetComponent<Bullet>())
        {
            newBullet.GetComponent<Bullet>().bm = this;
        }else if (newBullet.GetComponent<EnemyBullet>())
        {
            newBullet.GetComponent<EnemyBullet>().bm = this;
            if (newBullet.GetComponent<Renderer>().isVisible == false){
                newBullet.GetComponent<EnemyBullet>().toDestroy = true;
            }
        }
        if (bullets.Count > 0 && name == "BulletManager")
        {
            bullets[bullets.Count - 1].GetComponent<Bullet>().homing = true;
            bullets[bullets.Count - 1].layer=10;
            bullets[bullets.Count - 1].GetComponent<SpriteRenderer>().sprite = homingSprite;
        }
        bullets.Add(newBullet);
    }
}
