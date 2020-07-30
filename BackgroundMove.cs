using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public GameObject background3;
    public GameObject background4;
    Renderer b1Rend;
    Renderer b2Rend;
    Renderer b3Rend;
    Renderer b4Rend;
    public GameObject player;
    public float borderDistance;
    // Start is called before the first frame update
    void Awake()
    {
        b1Rend = background1.GetComponent<Renderer>();
        b2Rend = background2.GetComponent<Renderer>();
        b3Rend = background3.GetComponent<Renderer>();
        b4Rend = background4.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.transform.position - transform.position;
        //Debug.Log("Player: "+player.transform.position);
        //Debug.Log("Background: "+transform.position);
        //Debug.Log("Distance: "+distance);
        if (distance.x > borderDistance)
        {
            transform.position = new Vector3(transform.position.x+ borderDistance, transform.position.y, transform.position.z);
        }
        else if (distance.x < 0)
        {
            transform.position = new Vector3(transform.position.x - borderDistance, transform.position.y, transform.position.z);
        }
        if (distance.y > borderDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + borderDistance, transform.position.z);
        }
        else if (distance.y < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - borderDistance, transform.position.z);
        }
    }
}
