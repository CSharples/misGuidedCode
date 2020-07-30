using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject player;
    public GameObject enemyBM;
    public float minShot;
    public float maxShot;
    float time_since_shot;
    AudioSource aud;
    float next_shot;
    public int pointValue;
    public GameObject deadShip;
    public bool shield;
    float speed=50f;
    void Awake()
    {
        aud=GetComponent<AudioSource>();
        next_shot = Random.Range(minShot, maxShot);
        //Debug.Log(next_shot);
        time_since_shot = Time.time;
        //Debug.Log(next_shot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time_since_shot > next_shot && GetComponent<Renderer>().isVisible)
        {
            Fire();
            next_shot = Random.Range(minShot, maxShot);
            time_since_shot = Time.time;
            //Debug.Log(next_shot);
        }
        Vector3 lookVector = player.transform.position;
        transform.rotation = Quaternion.LookRotation(transform.forward, lookVector - transform.position);
        
    }
    void FixedUpdate(){
        if(!shield){
            //Move();
        }
    }
    void Move(){
        float step =  speed * Time.deltaTime; // calculate distance to move
        GetComponent<Rigidbody2D>().velocity=((player.transform.position - transform.position).normalized * speed * Time.smoothDeltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }
    public void Fire()
    {
        //Debug.Log("Bang");
        aud.Play();
        enemyBM.GetComponent<BulletManager>().Fire(gameObject);
    }

    public void Hit(){
        //Debug.Log("I'm Hit!");
        player.GetComponent<GameController>().Score(pointValue);
        Instantiate(deadShip,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<GameController>().Die();
            Hit();
            //Debug.Log("Killed by own bullet");
        }
    }
}
