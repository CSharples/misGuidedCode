using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 350f;
    public Rigidbody2D rb;
    public bool homing = false;
    public BulletManager bm;
    public bool toDestroy = false;
    public float offTime;
    public float tThreshold;
    public GameObject glint;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toDestroy == true && Time.time - offTime > tThreshold)
        {
            SelfDestruct();
        }
    }

    public void Move()
    {
        rb.velocity = transform.up * MoveSpeed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log("Ouch!");
        if (collision.gameObject.tag=="Enemy"){
            collision.gameObject.GetComponent<Enemy1>().Hit();
        }
        else if(collision.gameObject.tag=="Player"){
            collision.gameObject.GetComponent<GameController>().Die();
        }
        else
        {
            Instantiate(glint, transform.position, Quaternion.identity);
            SelfDestruct();
        }
        SelfDestruct();
    }
    public void SelfDestruct()
    {
        bm.bullets.Remove(gameObject);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        toDestroy = true;
        offTime = Time.time;
    }

    void OnBecameVisible()
    {
        toDestroy = false;
    }
}

